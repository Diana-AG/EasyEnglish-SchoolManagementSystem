namespace EasyEnglish.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;
    using Microsoft.EntityFrameworkCore;

    public class CourseService : ICourseService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public CourseService(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.coursesRepository = coursesRepository;
            this.usersRepository = usersRepository;
        }

        public async Task<ApplicationUser> GetUserByIdAsync(string id)
        {
            return await this.usersRepository.All().Include(x => x.StudentCourses).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Course> GetCourseByIdAsync(int id)
        {
            return await this.coursesRepository.All().Include(x => x.Teacher).Include(x => x.CourseType).Include(x => x.Students).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<CourseViewModel> GetCourseViewModelByIdAsync(int? id)
        {
            var course = await this.AllCourses().FirstOrDefaultAsync(x => x.Id == id);

            return course;
        }

        public async Task AddStudentAsync(CourseStudentInputModel input)
        {
            var course = await this.GetCourseByIdAsync((int)input.CourseId);
            var student = await this.GetUserByIdAsync(input.StudentId);

            if (!course.Students.Any(x => x.Id == student.Id))
            {
                course.Students.Add(student);

                this.coursesRepository.Update(course);
                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public async Task RemoveStudentAsync(CourseStudentInputModel input)
        {
            var course = await this.GetCourseByIdAsync((int)input.CourseId);
            var student = await this.GetUserByIdAsync(input.StudentId);

            if (course.Students.Any(x => x.Id == student.Id))
            {
                course.Students.Remove(student);

                this.coursesRepository.Update(course);
                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public IQueryable<CourseAddStudentViewModel> AllStudents(int id)
        {
            var students = this.usersRepository.All()
                .Where(x => !x.StudentCourses.Any(sc => sc.Id == id))
                .OrderBy(x => x.FullName)
                .Select(x => new CourseAddStudentViewModel
                {
                    CourseId = (int)id,
                    StudentId = x.Id,
                    StudentName = x.FullName,
                    StudentEmail = x.Email,
                });

            return students;
        }

        public IQueryable<CourseViewModel> AllCourses()
        {
            var courses = this.coursesRepository.All()
                .Include(c => c.CourseType)
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .Select(c => new CourseViewModel
                {
                    Id = c.Id,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Price = c.Price,
                    Description = c.Description,
                    CourseType = $"{c.CourseType.Language.Name} - {c.CourseType.Level.Name}",
                    Teacher = c.Teacher.FullName,
                    //CourseType = new CourseTypeViewModel
                    //{
                    //    Id = c.CourseTypeId,
                    //    Name = $"{c.CourseType.Language.Name} - {c.CourseType.Level.Name}",
                    //},
                    //Teacher = new TeacherViewModel
                    //{
                    //    Id = c.TeacherId,
                    //    FullName = c.Teacher.FullName,
                    //},
                    Students = c.Students
                    .OrderBy(s => s.FullName)
                    .Select(s => new StudentViewModel
                    {
                        Id = s.Id,
                        Email = s.Email,
                        Name = s.FullName,
                        BirthDate = (DateTime)s.BirthDate,
                    }),
                    StudentsCount = c.Students.Count(),
                });

            return courses;
        }

        public async Task CreateCourseAsync(CourseInputModel input)
        {
            var course = new Course
            {
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                Price = input.Price,
                TeacherId = input.TeacherId,
                CourseTypeId = input.CourseTypeId,
                Description = input.Description,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task EditCourseAsync(CourseInputModel input)
        {
            var course = await this.GetCourseByIdAsync(input.Id);
            course.StartDate = input.StartDate;
            course.EndDate = input.EndDate;
            course.Price = input.Price;
            course.TeacherId = input.TeacherId;
            course.CourseTypeId = input.CourseTypeId;
            course.Description = input.Description;

            this.coursesRepository.Update(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await this.GetCourseByIdAsync(id);
            this.coursesRepository.Delete(course);
            await this.coursesRepository.SaveChangesAsync();
        }
    }
}

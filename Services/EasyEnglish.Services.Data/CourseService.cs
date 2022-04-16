namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
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

        public async Task CreateAsync(CourseInputModel input, string userId)
        {
            var course = new Course
            {
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                TeacherId = userId,
                TrainingFormId = input.TrainingFormId,
                CourseTypeId = input.CourseTypeId,
                Description = input.Description,
            };

            await this.coursesRepository.AddAsync(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var course = await this.coursesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.coursesRepository.Delete(course);
            await this.coursesRepository.SaveChangesAsync();
        }

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 8)
        {
            var courses = this.coursesRepository.AllAsNoTracking()
                .Include(c => c.CourseType)
                .Include(c => c.Teacher)
                .Include(c => c.Students)
                .OrderBy(x => x.Teacher.Name)
                .ThenBy(x => x.CourseType.Language.Name)
                .ThenBy(x => x.CourseType.Level.Name)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return courses;
        }

        public IEnumerable<CourseAddStudentViewModel> AllStudents(int id)
        {
            var students = this.usersRepository.All()
                .Where(x => !x.StudentCourses.Any(sc => sc.Id == id))
                .OrderBy(x => x.Name)
                .Select(x => new CourseAddStudentViewModel
                {
                    CourseId = id,
                    StudentId = x.Id,
                    StudentName = x.Name,
                    StudentEmail = x.Email,
                })
                .ToList();

            return students;
        }

        public T GetById<T>(int id)
        {
            var course = this.coursesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return course;
        }

        public T GetById<T>(string id)
        {
            var student = this.usersRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefault();

            return student;
        }

        public int GetCount()
        {
            return this.coursesRepository.All().Count();
        }

        public async Task AddStudentAsync(CourseStudentInputModel input)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == (int)input.CourseId);
            var student = this.usersRepository.All().FirstOrDefault(x => x.Id == input.StudentId);

            if (!course.Students.Any(x => x.Id == student.Id))
            {
                course.Students.Add(student);

                this.coursesRepository.Update(course);
                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public async Task RemoveStudentAsync(CourseStudentInputModel input)
        {
            var course = this.coursesRepository.All().Include(x => x.Students).FirstOrDefault(x => x.Id == (int)input.CourseId);
            var student = this.usersRepository.All().FirstOrDefault(x => x.Id == input.StudentId);

            if (course.Students.Any(x => x.Id == student.Id))
            {
                course.Students.Remove(student);

                this.coursesRepository.Update(course);
                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public async Task UpdateAsync(int id, EditCourseInputModel input)
        {
            var course = this.coursesRepository.All().FirstOrDefault(x => x.Id == id);
            course.StartDate = input.StartDate;
            course.EndDate = input.EndDate;
            course.TrainingFormId = input.TrainingFormId;
            course.CourseTypeId = input.CourseTypeId;
            course.Description = input.Description;

            await this.coursesRepository.SaveChangesAsync();
        }
    }
}

namespace EasyEnglish.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
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

        public async Task CreateAsync(CourseInputModel input, string userId)
        {
            var course = new Course
            {
                StartDate = input.StartDate,
                EndDate = input.EndDate,
                Price = input.Price,
                TeacherId = userId,
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

        public IEnumerable<T> GetAll<T>(int page, int itemsPerPage = 12)
        {
            var courses = this.coursesRepository.AllAsNoTracking()
                .Include(c => c.CourseType)
                .Include(c => c.Teacher)
                .Include(c => c.Students)

                // .Select(c => new CourseInListViewModel
                // {
                //    Id = c.Id,
                //    StartDate = c.StartDate,
                //    EndDate = c.EndDate,
                //    Price = c.Price,
                //    Description = c.Description,
                //    CourseType = $"{c.CourseType.Language.Name} - {c.CourseType.Level.Name}",
                //    Teacher = c.Teacher.FullName,
                //    Students = c.Students
                //    .OrderBy(s => s.FullName)
                //    .Select(s => new StudentViewModel
                //    {
                //        Id = s.Id,
                //        Email = s.Email,
                //        Name = s.FullName,
                //        BirthDate = (DateTime)s.BirthDate,
                //    }),
                //    StudentsCount = c.Students.Count(),
                // });
                .OrderBy(x => x.Teacher.FullName)
                .ThenBy(x => x.CourseType.Language.Name)
                .ThenBy(x => x.CourseType.Level.Name)
                .Skip((page - 1) * itemsPerPage).Take(itemsPerPage)
                .To<T>().ToList();

            return courses;
        }

        public IEnumerable<T> AllStudents<T>(int id)
        {
            var students = this.usersRepository.All()
                .Where(x => !x.StudentCourses.Any(sc => sc.Id == id))
                .OrderBy(x => x.FullName)
                .To<T>().ToList();

                // .Select(x => new CourseAddStudentViewModel
                // {
                //    CourseId = (int)id,
                //    StudentId = x.Id,
                //    StudentName = x.FullName,
                //    StudentEmail = x.Email,
                // });
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
            var course = this.GetById<Course>((int)input.CourseId);
            var student = this.GetById<ApplicationUser>(input.StudentId);

            if (!course.Students.Any(x => x.Id == student.Id))
            {
                course.Students.Add(student);

                this.coursesRepository.Update(course);
                await this.coursesRepository.SaveChangesAsync();
            }
        }

        public async Task RemoveStudentAsync(CourseStudentInputModel input)
        {
            var course = this.GetById<Course>((int)input.CourseId);
            var student = this.GetById<ApplicationUser>(input.StudentId);

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
            course.Price = input.Price;
            course.CourseTypeId = input.CourseTypeId;
            course.Description = input.Description;

            await this.coursesRepository.SaveChangesAsync();
        }
    }
}

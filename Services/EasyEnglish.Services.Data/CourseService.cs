namespace EasyEnglish.Services.Data
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using Microsoft.EntityFrameworkCore;

    public class CourseService : ICourseService
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRespository;

        public CourseService(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRespository)
        {
            this.coursesRepository = coursesRepository;
            this.usersRespository = usersRespository;
        }

        public async Task AddStudent(CourseAddStudentInputModel input)
        {
            var course = this.coursesRepository.All().Include(c => c.Students).FirstOrDefault(x => x.Id == input.CourseId);
            var student = this.usersRespository.All().FirstOrDefault(x => x.Id == input.StudentId);

            //if (course == null || student == null)
            //{

            //}

            if (!course.Students.Any(x => x.Id == student.Id))
            {
                course.Students.Add(student);

                this.coursesRepository.Update(course);
                await this.coursesRepository.SaveChangesAsync();
            }
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
                    CourseType = new CourseTypeViewModel
                    {
                        Id = c.CourseTypeId,
                        Name = $"{c.CourseType.Language.Name} - {c.CourseType.Level.Name}",
                    },
                    Teacher = new TeacherViewModel
                    {
                        Id = c.TeacherId,
                        FullName = c.Teacher.FullName,
                    },
                    Students = c.Students
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

        public async Task<CourseViewModel> GetCourseByIdAsync(int? id)
        {
            var course = await this.AllCourses().FirstOrDefaultAsync(x => x.Id == id);

            return course;
        }
    }
}

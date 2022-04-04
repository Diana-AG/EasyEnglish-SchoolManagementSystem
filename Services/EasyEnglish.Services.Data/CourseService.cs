namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public class CourseService : ICourseService
    {
        private readonly IDeletableEntityRepository<Course> coursesRespository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRespository;

        public CourseService(
            IDeletableEntityRepository<Course> coursessRespository,
            IDeletableEntityRepository<ApplicationUser> usersRespository)
        {
            this.coursesRespository = coursessRespository;
            this.usersRespository = usersRespository;
        }

        public async Task AddStudent(CourseAddStudentInputModel input)
        {
            var course = this.coursesRespository.All().FirstOrDefault(x => x.Id == input.CourseId);
            //var studentId = input.Students.Select(x => x.Id).FirstOrDefault();
            //var student = this.usersRespository.All().FirstOrDefault(x => x.Id == studentId);

            //if (course == null || student == null)
            //{

            //}

            //course.Students.Add(student);

            this.coursesRespository.Update(course);
            await this.coursesRespository.SaveChangesAsync();
        }
    }
}

namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public interface ICourseService
    {
        Task CreateCourseAsync(CourseInputModel input);

        Task EditCourseAsync(CourseInputModel input);

        Task AddStudentAsync(CourseStudentInputModel input);

        Task DeleteAsync(int id);

        Task RemoveStudentAsync(CourseStudentInputModel input);

        IQueryable<CourseViewModel> AllCourses();

        IQueryable<CourseAddStudentViewModel> AllStudents(int id);

        Task<Course> GetCourseByIdAsync(int id);

        Task<ApplicationUser> GetUserByIdAsync(string id);

        Task<CourseViewModel> GetCourseViewModelByIdAsync(int? id);
    }
}

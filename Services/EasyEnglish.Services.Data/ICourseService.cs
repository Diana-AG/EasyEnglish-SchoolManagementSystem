namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public interface ICourseService
    {
        Task AddStudent(CourseStudentInputModel input);

        Task RemoveStudent(CourseStudentInputModel input);

        IQueryable<CourseViewModel> AllCourses();

        Task<CourseViewModel> GetCourseByIdAsync(int? id);
    }
}

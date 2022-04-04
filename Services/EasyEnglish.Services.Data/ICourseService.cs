namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public interface ICourseService
    {
        Task AddStudent(CourseAddStudentInputModel input);

        IQueryable<CourseViewModel> AllCourses();

        Task<CourseViewModel> GetCourseByIdAsync(int? id);
    }
}

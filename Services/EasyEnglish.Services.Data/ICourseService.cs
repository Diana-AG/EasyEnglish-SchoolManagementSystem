namespace EasyEnglish.Services.Data
{
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public interface ICourseService
    {
        Task AddStudent(CourseAddStudentInputModel input);
    }
}

namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public interface ICourseService
    {
        Task CreateAsync(CourseInputModel input, string userId);

        Task<IEnumerable<T>> GetAll<T>(int page, int itemsPerPage = 8);

        int GetCount();

        Task<T> GetByIdAsync<T>(int id);

        Task<T> GetById<T>(string id);

        Task DeleteAsync(int id);

        Task UpdateAsync(int id, EditCourseInputModel input);

        Task AddStudentAsync(CourseStudentInputModel input);

        Task RemoveStudentAsync(CourseStudentInputModel input);

        Task<IEnumerable<CourseAddStudentViewModel>> GetAvailableStudents(int id);
    }
}

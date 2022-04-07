namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;

    public interface ICourseTypeService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();

        IQueryable<CourseTypeViewModel> AllCourseTypes();

        Task<CourseTypeViewModel> GetCourseTypeViewModelByIdAsync(int id);

        Task CreateCourseAsync(CourseTypeInputModel input);

        Task DeleteAsync(int id);

        Task<CourseType> GetCourseTypeByIdAsync(int? id);

        bool CourseTypeExists(int languageId, int levelId);
    }
}

namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;

    public interface ICourseTypesService
    {
        Task DeleteAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(CourseTypeInputModel input);

        Task UpdateAsync(int id, EditCourseTypeInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}

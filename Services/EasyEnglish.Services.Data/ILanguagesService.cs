namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Languages;

    public interface ILanguagesService
    {
        Task DeleteAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(LanguageInputModel input);

        Task UpdateAsync(int id, EditLanguageInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}

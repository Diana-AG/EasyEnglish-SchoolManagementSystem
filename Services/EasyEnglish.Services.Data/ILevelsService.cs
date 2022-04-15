namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Levels;

    public interface ILevelsService
    {
        Task DeleteAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task CreateAsync(LevelInputModel input);

        Task UpdateAsync(int id, EditLevelInputModel input);

        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();
    }
}

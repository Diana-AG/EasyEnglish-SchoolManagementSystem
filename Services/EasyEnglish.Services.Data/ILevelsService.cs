namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Levels;

    public interface ILevelsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();

        IEnumerable<T> GetAll<T>();

        Task CreateLevelAsync(LevelInputModel input);

        Task<Level> GetLevelByIdAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task DeleteAsync(int id);
    }
}

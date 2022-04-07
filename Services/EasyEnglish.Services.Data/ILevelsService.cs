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

        IQueryable<LevelViewModel> AllLevels();

        Task CreateLevelAsync(LevelInputModel input);

        Task<Level> GetLevelByIdAsync(int id);

        Task<LevelViewModel> GetLevelViewModelByIdAsync(int id);

        Task DeleteAsync(int id);
    }
}

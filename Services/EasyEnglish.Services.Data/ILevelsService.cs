namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    
    using EasyEnglish.Web.ViewModels.Administration.Levels;

    public interface ILevelsService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();

        IQueryable<LevelViewModel> AllLevels();
    }
}

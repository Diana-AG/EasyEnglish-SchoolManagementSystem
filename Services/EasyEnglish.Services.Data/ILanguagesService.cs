namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Web.ViewModels.Administration.Languages;

    public interface ILanguagesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();

        IQueryable<LanguageViewModel> AllLanguages();

        LanguageViewModel GetLanguageViewModelById(int? id);
    }
}

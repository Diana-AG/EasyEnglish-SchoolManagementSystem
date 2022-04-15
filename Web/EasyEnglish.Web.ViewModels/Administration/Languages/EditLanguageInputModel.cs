namespace EasyEnglish.Web.ViewModels.Administration.Languages
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class EditLanguageInputModel : LanguageInputModel, IMapFrom<Language>
    {
        public int Id { get; set; }
    }
}

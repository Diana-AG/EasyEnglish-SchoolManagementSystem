namespace EasyEnglish.Web.ViewModels.Administration.Languages
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class LanguageViewModel : IMapFrom<Language>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

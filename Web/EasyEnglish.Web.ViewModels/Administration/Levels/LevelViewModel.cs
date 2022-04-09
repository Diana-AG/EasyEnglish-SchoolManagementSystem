namespace EasyEnglish.Web.ViewModels.Administration.Levels
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class LevelViewModel : IMapFrom<Level>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}

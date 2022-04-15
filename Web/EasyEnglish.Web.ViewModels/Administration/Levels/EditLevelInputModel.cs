namespace EasyEnglish.Web.ViewModels.Administration.Levels
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class EditLevelInputModel : LevelInputModel, IMapFrom<Level>
    {
        public int Id { get; set; }
    }
}

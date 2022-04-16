namespace EasyEnglish.Web.ViewModels.Administration.CourseTypes
{
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class CourseTypeViewModel : IMapFrom<CourseType>
    {
        public int Id { get; set; }

        [Display(Name = "Language")]
        public string LanguageName { get; set; }

        [Display(Name = "Level")]
        public string LevelName { get; set; }

        public string Description { get; set; }
    }
}

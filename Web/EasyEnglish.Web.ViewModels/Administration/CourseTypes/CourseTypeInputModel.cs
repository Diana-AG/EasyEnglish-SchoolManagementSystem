namespace EasyEnglish.Web.ViewModels.Administration.CourseTypes
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseTypeInputModel
    {
        [Display(Name = "Language")]
        public int LanguageId { get; set; }

        [Display(Name = "Level")]
        public int LevelId { get; set; }

        [Required]
        [MinLength(10)]
        [MaxLength(10000)]
        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LevelsItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> LanguagesItems { get; set; }
    }
}

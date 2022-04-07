namespace EasyEnglish.Web.ViewModels.Administration.CourseTypes
{
    using System.ComponentModel.DataAnnotations;

    public class CourseTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Language")]
        public string Language { get; set; }

        [Display(Name = "Level")]
        public string Level { get; set; }

        public string Description { get; set; }
    }
}

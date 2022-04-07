namespace EasyEnglish.Web.ViewModels.Administration.CourseTypes
{
    using System.ComponentModel.DataAnnotations;

    public class CourseTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Language - Level")]
        public string Name { get; set; }
    }
}

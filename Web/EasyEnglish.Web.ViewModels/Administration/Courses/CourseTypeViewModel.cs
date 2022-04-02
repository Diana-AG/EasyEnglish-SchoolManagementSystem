namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class CourseTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Language - Level")]
        public string Name { get; set; }
    }
}

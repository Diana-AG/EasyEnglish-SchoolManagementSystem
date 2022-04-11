namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class TeacherViewModel
    {
        public string Id { get; set; }

        [Display(Name = "Teacher")]
        public string Name { get; set; }
    }
}

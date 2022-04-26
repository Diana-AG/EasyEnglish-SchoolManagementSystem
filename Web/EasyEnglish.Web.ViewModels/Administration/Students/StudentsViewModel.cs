namespace EasyEnglish.Web.ViewModels.Administration.Students
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class StudentsViewModel : IMapFrom<ApplicationUser>
    {
        public string Id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        public string Email { get; set; }

        [Display(Name = "Birth Date")]
        public DateTime BirthDate { get; set; }
    }
}

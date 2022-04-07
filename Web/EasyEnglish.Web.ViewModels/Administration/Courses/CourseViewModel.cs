namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class CourseViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        public decimal Price { get; set; }

        public string Teacher { get; set; }

        [Display(Name = "Course Type")]
        public string CourseType { get; set; }

        [Display(Name = "Students")]
        public IEnumerable<StudentViewModel> Students { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Description { get; set; }

        [Display(Name = "Students Count")]
        public int StudentsCount { get; set; }
    }
}

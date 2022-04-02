namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
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

        public TeacherViewModel Teacher { get; set; }

        public CourseTypeViewModel CourseType { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}

namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using EasyEnglish.Data.Models;

    public class CourseInputModel
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Range(typeof(decimal), "0.01", "10000000")]
        public decimal Price { get; set; }

        [Display(Name = "Teacher")]
        public string TeacherId { get; set; }

        public ApplicationUser Teacher { get; set; }

        [Display(Name = "Language-Level")]
        public int CourseTypeId { get; set; }

        public CourseType CourseType { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Description { get; set; }
    }
}

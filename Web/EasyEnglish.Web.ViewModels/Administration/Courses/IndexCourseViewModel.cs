namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class IndexCourseViewModel
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

    public class TeacherViewModel 
    {
        public string Id { get; set; }

        [Display(Name = "Teacher")]
        public string Name { get; set; }
    }

    public class CourseTypeViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Language - Level")]
        public string Name { get; set; }
    }
}

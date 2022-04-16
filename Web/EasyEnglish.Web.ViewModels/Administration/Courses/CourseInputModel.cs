namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using EasyEnglish.Data.Models;

    public class CourseInputModel
    {

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Training Form")]
        public int TrainingFormId { get; set; }

        public TrainingForm TrainingForm { get; set; }

        [Display(Name = "Language-Level")]
        public int CourseTypeId { get; set; }

        public CourseType CourseType { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CourseTypeItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TrainingFormsItems { get; set; }
    }
}

namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.Infrastructure.ValidationAttributes;

    public class CourseInputModel : IValidatableObject
    {
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        [CurrentYearMaxValue(2000)]
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
        [MinLength(10)]
        [MaxLength(10000)]
        public string Description { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CourseTypeItems { get; set; }

        public IEnumerable<KeyValuePair<string, string>> TrainingFormsItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.EndDate <= this.StartDate)
            {
                yield return new ValidationResult(
                $"End date can not be before start date.",
                new[] { nameof(this.EndDate) });
            }
        }
    }
}

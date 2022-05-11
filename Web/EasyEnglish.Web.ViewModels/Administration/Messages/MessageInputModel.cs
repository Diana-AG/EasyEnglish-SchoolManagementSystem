namespace EasyEnglish.Web.ViewModels.Administration.Messages
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class MessageInputModel : IValidatableObject
    {
        [Required]
        [MaxLength(10000)]
        [MinLength(50)]
        [Display(Name = "Content")]
        public string Description { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Publish on")]
        public DateTime StartDate { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Active until")]
        public DateTime? EndDate { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.EndDate <= this.StartDate)
            {
                yield return new ValidationResult(
                $"End Date can not be before Start Date. Do not set End date if you wont message to be visible indefinitely",
                new[] { nameof(this.EndDate) });
            }
        }
    }
}

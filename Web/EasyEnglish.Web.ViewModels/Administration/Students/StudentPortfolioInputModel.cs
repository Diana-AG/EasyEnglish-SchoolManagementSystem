namespace EasyEnglish.Web.ViewModels.Administration.Students
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class StudentPortfolioInputModel : IValidatableObject
    {
        public string StudentId { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Images == null)
            {
                yield return new ValidationResult(
                $"There is need to choose a file.",
                new[] { nameof(this.Images) });
            }
        }
    }
}

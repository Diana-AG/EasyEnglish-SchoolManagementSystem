namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ResourceUploadFileInputModel : IMapTo<Resource>, IValidatableObject
    {
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Name { get; set; } = null;

        [Display(Name = "Language-Level")]
        public int CourseTypeId { get; set; }

        public CourseType CourseType { get; set; }

        [DisplayName("")]
        public IFormFile Image { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CourseTypeItems { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Image == null)
            {
                yield return new ValidationResult(
                $"There is need to choose a file.",
                new[] { nameof(this.Image) });
            }
        }
    }
}

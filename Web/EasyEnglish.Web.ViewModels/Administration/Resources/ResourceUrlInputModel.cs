namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class ResourceUrlInputModel : IMapTo<Resource>
    {
        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [Url]
        public string Url { get; set; }

        [Display(Name = "Language-Level")]
        public int CourseTypeId { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CourseTypeItems { get; set; }
    }
}

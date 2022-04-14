namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ResourceInputModel : IMapTo<Resource>
    {
        public string Name { get; set; }

        public string Url { get; set; }

        [Display(Name = "Language-Level")]
        public int CourseTypeIds { get; set; }

        [DisplayName("")]
        public IEnumerable<IFormFile> Images { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CourseTypeItems { get; set; }
    }
}

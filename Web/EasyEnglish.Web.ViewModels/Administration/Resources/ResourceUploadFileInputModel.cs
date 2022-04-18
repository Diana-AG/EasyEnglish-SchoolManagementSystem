namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using Microsoft.AspNetCore.Http;

    public class ResourceUploadFileInputModel : IMapTo<Resource>
    {
        public string Name { get; set; }


        [Display(Name = "Language-Level")]
        public int CourseTypeId { get; set; }

        public CourseType CourseType { get; set; }

        [DisplayName("")]
        public IFormFile Image { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CourseTypeItems { get; set; }
    }
}

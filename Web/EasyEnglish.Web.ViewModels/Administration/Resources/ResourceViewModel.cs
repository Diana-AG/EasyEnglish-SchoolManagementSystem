namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class ResourceViewModel : IMapFrom<Resource>
    {
        [Display(Name = "Remote Urls")]
        public ICollection<ResourceUrlViewModel> ResourceUrls { get; set; }

        [Display(Name = "Download Files")]
        public ICollection<ResourceFileViewModel> ResourceFiles { get; set; }
    }
}

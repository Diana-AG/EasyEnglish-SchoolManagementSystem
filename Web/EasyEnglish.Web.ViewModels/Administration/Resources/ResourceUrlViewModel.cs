namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class ResourceUrlViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Url { get; set; }
    }
}

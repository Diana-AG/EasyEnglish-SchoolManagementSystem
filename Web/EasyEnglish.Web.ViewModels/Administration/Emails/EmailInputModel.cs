namespace EasyEnglish.Web.ViewModels.Administration.Emails
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Microsoft.AspNetCore.Http;

    public class EmailInputModel
    {
        [Required]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }

        public IEnumerable<IFormFile> Attachments { get; set; }
    }
}

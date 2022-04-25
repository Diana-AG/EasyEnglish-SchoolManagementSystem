namespace EasyEnglish.Web.ViewModels.Administration.Emails
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class EmailInputModel
    {
        public string Subject { get; set; }

        public string Content { get; set; }

        public IEnumerable<IFormFile> Attachments { get; set; }
    }
}

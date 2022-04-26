namespace EasyEnglish.Web.ViewModels.Administration.Students
{
    using System.Collections.Generic;

    using Microsoft.AspNetCore.Http;

    public class StudentPortfolioInputModel
    {
        public string StudentId { get; set; }

        public IEnumerable<IFormFile> Images { get; set; }
    }
}

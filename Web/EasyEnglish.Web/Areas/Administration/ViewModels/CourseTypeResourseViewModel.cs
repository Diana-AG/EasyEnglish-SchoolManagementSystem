namespace EasyEnglish.Web.Areas.Administration.ViewModels
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Models;

    public class CourseTypeResourseViewModel
    {
        public int CourseId { get; set; }

        public IEnumerable<Resource> Resources { get; set; }
    }
}

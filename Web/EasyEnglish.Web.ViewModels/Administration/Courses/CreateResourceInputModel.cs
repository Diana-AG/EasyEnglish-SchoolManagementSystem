namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using EasyEnglish.Data.Models;
    using System.Collections.Generic;

    public class CreateResourceInputModel
    {
        public string Description { get; set; }

        public string Url { get; set; }

        public IEnumerable<ResourceCourseTypeInputModel> CourseTypes { get; set; }
    }

    public class ResourceCourseTypeInputModel
    {
        public string Description { get; set; }
    }
}

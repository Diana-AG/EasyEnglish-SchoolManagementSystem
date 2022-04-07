namespace EasyEnglish.Web.ViewModels.Administration.Resources
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Models;

    public class ResourceInputModel
    {
        public string Description { get; set; }

        public string Url { get; set; }

        public IEnumerable<KeyValuePair<string, string>> CourseTypeItems { get; set; }
    }

    public class ResourceCourseTypeInputModel
    {
        public int ID { get; set; }

        public string Description { get; set; }

        public bool Checked { get; set; }
    }
}

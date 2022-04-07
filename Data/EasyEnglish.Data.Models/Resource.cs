namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Resource : BaseDeletableModel<int>
    {
        public Resource()
        {
            this.CourseTypes = new HashSet<ResourceCourseType>();
        }

        public string Description { get; set; }

        public string Url { get; set; }

        public virtual ICollection<ResourceCourseType> CourseTypes { get; set; }
    }
}

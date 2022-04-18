namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Resource : BaseDeletableModel<int>
    {
        public Resource()
        {
            this.Homeworks = new HashSet<Homework>();
            this.CourseTypes = new HashSet<ResourceCourseType>();
        }

        public string Name { get; set; }

        public string Url { get; set; }

        public string Extension { get; set; }

        public string ContentType { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }

        public virtual ICollection<ResourceCourseType> CourseTypes { get; set; }
    }
}

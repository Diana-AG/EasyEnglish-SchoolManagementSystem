namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class CourseType : BaseDeletableModel<int>
    {
        public CourseType()
        {
            this.Prices = new HashSet<Price>();
            this.Courses = new HashSet<Course>();
            this.Resources = new HashSet<ResourceCourseType>();
        }

        // public string Name { get; set; }
        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public int LevelId { get; set; }

        public virtual Level Level { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Price> Prices { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<ResourceCourseType> Resources { get; set; }
    }
}

namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class CourseType : BaseDeletableModel<int>
    {
        public CourseType()
        {
            this.Courses = new HashSet<Course>();
            this.Resources = new HashSet<Resource>();
        }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public int LevelId { get; set; }

        public virtual Level Level { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}

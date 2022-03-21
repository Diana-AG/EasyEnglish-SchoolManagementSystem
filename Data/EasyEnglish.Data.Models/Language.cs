namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Language : BaseDeletableModel<int>
    {
        public Language()
        {
             this.Teachers = new HashSet<Teacher>();
             this.Courses = new HashSet<Course>();
        }

        public string Name { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}

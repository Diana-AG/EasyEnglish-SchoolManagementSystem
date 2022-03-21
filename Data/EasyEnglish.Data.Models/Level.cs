namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Level : BaseDeletableModel<int>
    {
        public Level()
        {
            this.Courses = new HashSet<Course>();
            this.Resources = new HashSet<Resource>();
        }

        public string Name { get; set; }

        public virtual ICollection<Course> Courses { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}

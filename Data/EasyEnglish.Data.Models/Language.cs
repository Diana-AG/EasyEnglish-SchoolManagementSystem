namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Language : BaseDeletableModel<int>
    {
        public Language()
        {
             this.Teachers = new HashSet<ApplicationUser>();
             this.Levels = new HashSet<CourseType>();
        }

        public string Name { get; set; }

        public virtual ICollection<ApplicationUser> Teachers { get; set; }

        public virtual ICollection<CourseType> Levels { get; set; }
    }
}

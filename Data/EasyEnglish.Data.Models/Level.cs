namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Level : BaseDeletableModel<int>
    {
        public Level()
        {
            this.Languages = new HashSet<CourseType>();
        }

        public string Name { get; set; }

        public ICollection<CourseType> Languages { get; set; }
    }
}

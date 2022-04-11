namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class TrainingForm : BaseDeletableModel<int>
    {
        public TrainingForm()
        {
            this.Prices = new HashSet<Price>();
            this.Courses = new HashSet<Course>();
        }

        public string Name { get; set; }

        public virtual ICollection<Price> Prices { get; set; }

        public virtual ICollection<Course> Courses { get; set; }
    }
}

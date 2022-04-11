namespace EasyEnglish.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Homework : BaseDeletableModel<int>
    {
        public Homework()
        {
            this.Resources = new HashSet<Resource>();
        }

        public DateTime Date { get; set; }

        public string Description { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public virtual ICollection<Resource> Resources { get; set; }
    }
}

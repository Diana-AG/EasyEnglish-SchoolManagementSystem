namespace EasyEnglish.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using EasyEnglish.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Students = new HashSet<Student>();
            this.Comments = new HashSet<Comment>();
            this.Payments = new HashSet<Payment>();
        }

        public string Name { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string TeacherId { get; set; }

        public virtual Teacher Teacher { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public int LevelId { get; set; }

        public virtual Level Level { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}

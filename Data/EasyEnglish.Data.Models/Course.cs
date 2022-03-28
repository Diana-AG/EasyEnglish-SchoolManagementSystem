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
            this.Students = new HashSet<ApplicationUser>();
            this.Comments = new HashSet<Comment>();
            this.Payments = new HashSet<Payment>();
        }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }

        public int CourseTypeId { get; set; }

        public virtual CourseType CourseType { get; set; }

        public string Description { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}

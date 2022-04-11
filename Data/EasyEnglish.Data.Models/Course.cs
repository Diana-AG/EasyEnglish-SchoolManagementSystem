namespace EasyEnglish.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Course : BaseDeletableModel<int>
    {
        public Course()
        {
            this.Comments = new HashSet<Comment>();
            this.Payments = new HashSet<Payment>();
            this.Homeworks = new HashSet<Homework>();
            this.Students = new HashSet<ApplicationUser>();
        }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int TrainingFormId { get; set; }

        public virtual TrainingForm TrainingForm { get; set; }

        public string TeacherId { get; set; }

        public virtual ApplicationUser Teacher { get; set; }

        public int CourseTypeId { get; set; }

        public virtual CourseType CourseType { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Comment> Comments { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }

        public virtual ICollection<Homework> Homeworks { get; set; }

        public virtual ICollection<ApplicationUser> Students { get; set; }
    }
}

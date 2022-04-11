namespace EasyEnglish.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using EasyEnglish.Data.Common.Models;

    public class Price : BaseDeletableModel<int>
    {
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public int StudentsCount { get; set; }

        public int TrainingId { get; set; }

        public virtual TrainingForm TrainingForm { get; set; }

        public int CourseTypeId { get; set; }

        public virtual CourseType CourseType { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }
    }
}

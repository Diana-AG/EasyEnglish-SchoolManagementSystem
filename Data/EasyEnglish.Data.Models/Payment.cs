namespace EasyEnglish.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    using EasyEnglish.Data.Common.Models;

    public class Payment : BaseDeletableModel<int>
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Amount { get; set; }

        public DateTime Date { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public int CourseId { get; set; }

        public virtual Course Course { get; set; }

        public int CurrencyId { get; set; }

        public virtual Currency Currency { get; set; }
    }
}

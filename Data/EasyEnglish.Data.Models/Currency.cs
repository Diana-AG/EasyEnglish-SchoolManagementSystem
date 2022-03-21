namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Currency : BaseDeletableModel<int>
    {
        public Currency()
        {
            this.Payments = new HashSet<Payment>();
        }

        public string CurrencyCode { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Payment> Payments { get; set; }
    }
}

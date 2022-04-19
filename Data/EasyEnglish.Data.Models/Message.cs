namespace EasyEnglish.Data.Models
{
    using System;
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public Message()
        {
            this.Votes = new HashSet<Vote>();
        }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string AddedByUserId { get; set; }

        public ApplicationUser AddedByUser { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}

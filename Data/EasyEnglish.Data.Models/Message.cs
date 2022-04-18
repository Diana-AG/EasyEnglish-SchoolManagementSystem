namespace EasyEnglish.Data.Models
{
    using System;

    using EasyEnglish.Data.Common.Models;

    public class Message : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}

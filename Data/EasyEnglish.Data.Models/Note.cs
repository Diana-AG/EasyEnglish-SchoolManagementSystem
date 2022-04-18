namespace EasyEnglish.Data.Models
{
    using System;

    using EasyEnglish.Data.Common.Models;

    public class Note : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public bool IsPublic { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}

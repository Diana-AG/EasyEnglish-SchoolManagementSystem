namespace EasyEnglish.Data.Models
{
    using System;

    using EasyEnglish.Data.Common.Models;

    public class Comment : BaseDeletableModel<int>
    {
        public string Description { get; set; }

        public DateTime MyProperty { get; set; }

        public int CourseId { get; set; }

        public Course Course { get; set; }
    }
}

namespace EasyEnglish.Data.Models
{
    using System;

    using EasyEnglish.Data.Common.Models;

    public class Image : BaseDeletableModel<string>
    {
        public Image()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string StudentId { get; set; }

        public virtual Student Student { get; set; }

        public string Extension { get; set; }
    }
}

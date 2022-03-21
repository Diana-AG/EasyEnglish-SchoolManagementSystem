namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Resource : BaseDeletableModel<int>
    {
        public Resource()
        {
            this.Levels = new HashSet<Level>();
        }

        public string Url { get; set; }

        public int LanguageId { get; set; }

        public virtual Language Language { get; set; }

        public virtual ICollection<Level> Levels { get; set; }
    }
}

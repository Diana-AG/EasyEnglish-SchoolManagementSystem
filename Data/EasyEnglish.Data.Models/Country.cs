namespace EasyEnglish.Data.Models
{
    using System.Collections.Generic;

    using EasyEnglish.Data.Common.Models;

    public class Country : BaseDeletableModel<int>
    {
        public Country()
        {
            this.Towns = new HashSet<Town>();
        }

        public string Name { get; set; }

        public virtual ICollection<Town> Towns { get; set; }
    }
}

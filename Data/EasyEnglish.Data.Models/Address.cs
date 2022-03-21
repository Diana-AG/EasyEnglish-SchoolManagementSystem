namespace EasyEnglish.Data.Models
{
    using EasyEnglish.Data.Common.Models;

    public class Address : BaseDeletableModel<int>
    {
        public string Name { get; set; }

        public int TownId { get; set; }

        public virtual Town Town { get; set; }
    }
}

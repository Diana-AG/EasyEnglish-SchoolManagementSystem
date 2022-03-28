namespace EasyEnglish.Data.Models
{
    using EasyEnglish.Data.Common.Models;

    public class Level : BaseDeletableModel<int>
    {
        public string Name { get; set; }
    }
}

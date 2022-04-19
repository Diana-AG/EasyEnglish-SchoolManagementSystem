namespace EasyEnglish.Data.Models
{
    using EasyEnglish.Data.Common.Models;

    public class Vote : BaseModel<int>
    {
        public int? MessageId { get; set; }

        public virtual Message Message { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }

        public byte Value { get; set; }
    }
}

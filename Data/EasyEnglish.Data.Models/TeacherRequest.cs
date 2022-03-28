namespace EasyEnglish.Data.Models
{
    using EasyEnglish.Data.Common.Models;

    public class TeacherRequest : BaseDeletableModel<int>
    {
        public string UserId { get; set; }

        public ApplicationUser User { get; set; }
    }
}

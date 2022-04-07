namespace EasyEnglish.Data.Models
{
    using EasyEnglish.Data.Common.Models;

    public class ResourceCourseType : BaseDeletableModel<int>
    {
        public int ResourceId { get; set; }

        public Resource Resource { get; set; }

        public int CourseTypeId { get; set; }

        public CourseType CourseType { get; set; }
    }
}

namespace EasyEnglish.Data.Models
{
    public class ResourceCourseType
    {
        public int Id { get; set; }
      
        public int ReourceId { get; set; }

        public Resource Resource { get; set; }

        public int CourseTypeId { get; set; }

        public CourseType CourseType { get; set; }
    }
}

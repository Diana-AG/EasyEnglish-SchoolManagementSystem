namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class EditCourseInputModel : CourseInputModel,
        IMapFrom<Course>
    {
        public int Id { get; set; }
    }
}

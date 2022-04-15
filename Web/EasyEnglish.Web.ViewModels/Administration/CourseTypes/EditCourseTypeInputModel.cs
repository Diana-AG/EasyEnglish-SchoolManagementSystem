namespace EasyEnglish.Web.ViewModels.Administration.CourseTypes
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class EditCourseTypeInputModel : CourseTypeInputModel, IMapFrom<CourseType>
    {
        public int Id { get; set; }
    }
}

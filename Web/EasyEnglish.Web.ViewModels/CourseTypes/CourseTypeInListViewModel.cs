namespace EasyEnglish.Web.ViewModels.CourseTypes
{
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class CourseTypeInListViewModel : IMapFrom<CourseType>
    {
        public int Id { get; set; }

        public string LanguageName { get; set; }

        public string LevelName { get; set; }

        public string Description { get; set; }
    }
}

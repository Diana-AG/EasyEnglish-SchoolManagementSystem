namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;

    public interface ICourseTypeService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();

        IQueryable<CourseTypeViewModel> AllCourseTypes();
    }
}

namespace EasyEnglish.Web.ViewModels.CourseTypes
{
    using System.Collections.Generic;

    public class CourseTypeListViewModel
    {
        public IEnumerable<CourseTypeInListViewModel> CourseTypes { get; set; }
    }
}

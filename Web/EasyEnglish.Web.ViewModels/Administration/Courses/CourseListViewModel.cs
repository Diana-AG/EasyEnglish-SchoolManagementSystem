namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System.Collections.Generic;

    public class CourseListViewModel : PagingViewModel
    {
        public IEnumerable<CourseInListViewModel> Courses { get; set; }
    }
}

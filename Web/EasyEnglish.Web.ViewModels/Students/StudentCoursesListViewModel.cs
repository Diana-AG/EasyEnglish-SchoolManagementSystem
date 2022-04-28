namespace EasyEnglish.Web.ViewModels.Students
{
    using System.Collections.Generic;

    public class StudentCoursesListViewModel
    {
        public IEnumerable<StudentCoursesInListViewModel> Courses { get; set; }
    }
}

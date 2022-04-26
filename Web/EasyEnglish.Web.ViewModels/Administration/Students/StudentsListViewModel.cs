namespace EasyEnglish.Web.ViewModels.Administration.Students
{
    using System.Collections.Generic;

    public class StudentsListViewModel : PagingViewModel
    {
        public IEnumerable<StudentsViewModel> Students { get; set; }
    }
}

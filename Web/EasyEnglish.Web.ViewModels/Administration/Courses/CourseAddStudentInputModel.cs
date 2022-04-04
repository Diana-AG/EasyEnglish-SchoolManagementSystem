namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System.Collections.Generic;

    public class CourseAddStudentInputModel
    {
        public int CourseId { get; set; }

        public string StudentId { get; set; }

        public string StudentName { get; set; }

        public string StudentEmail { get; set; }
    }
}

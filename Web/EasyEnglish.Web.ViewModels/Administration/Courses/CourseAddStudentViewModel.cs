namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using AutoMapper;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class CourseAddStudentViewModel
    {
        public int CourseId { get; set; }

        public string StudentId { get; set; }

        public string StudentFullName { get; set; }

        public string StudentEmail { get; set; }
    }
}

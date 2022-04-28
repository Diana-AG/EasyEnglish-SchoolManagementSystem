namespace EasyEnglish.Web.ViewModels.Students
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class StudentCoursesInListViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Teacher")]
        public string TeacherName { get; set; }

        [Display(Name = "Course Type")]
        public string CourseType { get; set; }

        [Display(Name = "Training Form")]
        public string TrainingFormName { get; set; }

        [Display(Name = "Students")]
        public string Students { get; set; }

        public string Description { get; set; }

        [Display(Name = "Students Count")]
        public int StudentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, StudentCoursesInListViewModel>()
                  .ForMember(x => x.CourseType, options =>
                      options.MapFrom(x => $"{x.CourseType.Language.Name} - {x.CourseType.Level.Name}"))
                  .ForMember(x => x.StudentsCount, options =>
                      options.MapFrom(x => x.Students.Count))
                  .ForMember(x => x.Students, options =>
                      options.MapFrom(x => string.Join(", ", x.Students.Select(x => x.Name))));
        }
    }
}

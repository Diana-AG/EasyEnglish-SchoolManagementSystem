namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using AutoMapper;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;

    public class CourseInListViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Teacher")]
        public string TeacherFullName { get; set; }

        [Display(Name = "Course Type")]
        public string CourseType { get; set; }

        [Display(Name = "Students")]
        public string Students { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Description { get; set; }

        [Display(Name = "Students Count")]
        public int StudentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, CourseInListViewModel>()
                  .ForMember(x => x.CourseType, options =>
                      options.MapFrom(x => $"{x.CourseType.Language.Name} - {x.CourseType.Level.Name}"))
                  .ForMember(x => x.StudentsCount, options =>
                      options.MapFrom(x => x.Students.Count))
                  .ForMember(x => x.Students, options =>
                      options.MapFrom(x => string.Join(", ", x.Students.Select(x => x.FullName))));
        }
    }
}

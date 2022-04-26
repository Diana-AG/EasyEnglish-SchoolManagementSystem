namespace EasyEnglish.Web.ViewModels.Administration.Courses
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using AutoMapper;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.Students;

    public class CourseViewModel : IMapFrom<Course>, IHaveCustomMappings
    {
        public int Id { get; set; }

        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Display(Name = "Teacher")]
        public string TeacherName { get; set; }

        [Display(Name = "Training Form")]
        public string TrainingFormName { get; set; }

        [Display(Name = "Course Type")]
        public string CourseType { get; set; }

        [Display(Name = "Students")]
        public IEnumerable<StudentsViewModel> Students { get; set; }

        [Required]
        [MinLength(4)]
        [MaxLength(100)]
        public string Description { get; set; }

        [Display(Name = "Students Count")]
        public int StudentsCount { get; set; }

        public void CreateMappings(IProfileExpression configuration)
        {
            configuration.CreateMap<Course, CourseViewModel>()
                  .ForMember(x => x.CourseType, options =>
                      options.MapFrom(x => $"{x.CourseType.Language.Name} - {x.CourseType.Level.Name}"))
                  .ForMember(x => x.StudentsCount, options =>
                      options.MapFrom(x => x.Students.Count));
        }
    }
}

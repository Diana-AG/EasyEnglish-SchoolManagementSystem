namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;
    using Microsoft.EntityFrameworkCore;

    public class CourseTypeService : ICourseTypeService
    {
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;

        public CourseTypeService(IDeletableEntityRepository<CourseType> courseTypesRepository)
        {
            this.courseTypesRepository = courseTypesRepository;
        }

        public IQueryable<CourseTypeViewModel> AllCourseTypes()
        {
            var courseTypes = this.courseTypesRepository.All()
                .Include(x => x.Language)
                .Include(x => x.Level)
                .Select(x => new CourseTypeViewModel
                {
                    Id = x.Id,
                    Name = $"{x.Language.Name} - {x.Level.Name}",
                });

            return courseTypes;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.courseTypesRepository.All()
                .OrderBy(x => x.Language.Name)
                .ThenBy(x => x.Level.Name)
                .Select(x => new
                {
                    Id = x.Id,
                    Name = $"{x.Language.Name} - {x.Level.Name}",
                }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}

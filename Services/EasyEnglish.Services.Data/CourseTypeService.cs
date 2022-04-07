namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

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
                .OrderBy(x => x.Language.Name)
                .ThenBy(x => x.Level.Name)
                .Select(x => new CourseTypeViewModel
                {
                    Id = x.Id,
                    Language = x.Language.Name,
                    Level = x.Level.Name,
                    Description = x.Description
                });

            return courseTypes;
        }

        public async Task CreateCourseAsync(CourseTypeInputModel input)
        {
            var courseType = new CourseType
            {
                LanguageId = input.LanguageId,
                LevelId = input.LevelId,
                Description = input.Description,
            };

            await this.courseTypesRepository.AddAsync(courseType);
            await this.courseTypesRepository.SaveChangesAsync();
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

        public async Task<CourseType> GetCourseTypeByIdAsync(int? id)
        {
            return await this.courseTypesRepository.All().Include(x => x.Language).Include(x => x.Level).FirstOrDefaultAsync(x => x.Id == id);
        }

        public bool CourseTypeExists(int languageId, int levelId)
        {
            return this.courseTypesRepository.All().Any(x => x.LanguageId == languageId && x.LevelId == levelId);
        }

        public async Task<CourseTypeViewModel> GetCourseTypeViewModelByIdAsync(int id)
        {
            var course = await this.AllCourseTypes().FirstOrDefaultAsync(x => x.Id == id);

            return course;
        }

        public async Task DeleteAsync(int id)
        {
            var courseType = await this.GetCourseTypeByIdAsync(id);
            this.courseTypesRepository.Delete(courseType);
            await this.courseTypesRepository.SaveChangesAsync();
        }
    }
}

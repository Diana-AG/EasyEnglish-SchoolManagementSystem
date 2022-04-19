namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;
    using Microsoft.EntityFrameworkCore;

    public class CourseTypesService : ICourseTypesService
    {
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;

        public CourseTypesService(IDeletableEntityRepository<CourseType> courseTypesRepository)
        {
            this.courseTypesRepository = courseTypesRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var courseType = await this.courseTypesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.courseTypesRepository.Delete(courseType);
            await this.courseTypesRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var courseType = await this.courseTypesRepository.AllAsNoTracking()
                           .Where(x => x.Id == id)
                           .To<T>().FirstOrDefaultAsync();

            return courseType;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var courseTypes = await this.courseTypesRepository.AllAsNoTracking()
                .Include(x => x.Language)
                .Include(x => x.Level)
                .OrderBy(x => x.Language.Name)
                .ThenBy(x => x.Level.Name)
                .To<T>().ToListAsync();

            return courseTypes;
        }

        public async Task CreateAsync(CourseTypeInputModel input)
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

        public async Task UpdateAsync(int id, EditCourseTypeInputModel input)
        {
            var courseType = this.courseTypesRepository.All().FirstOrDefault(x => x.Id == id);
            courseType.LanguageId = input.LanguageId;
            courseType.LevelId = input.LevelId;
            courseType.Description = input.Description;

            await this.courseTypesRepository.SaveChangesAsync();
        }
    }
}

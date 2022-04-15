namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.Levels;
    using Microsoft.EntityFrameworkCore;

    public class LevelsService : ILevelsService
    {
        private readonly IDeletableEntityRepository<Level> levelsRepository;

        public LevelsService(IDeletableEntityRepository<Level> levelsRepository)
        {
            this.levelsRepository = levelsRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var level = await this.levelsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.levelsRepository.Delete(level);
            await this.levelsRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var level = await this.levelsRepository.AllAsNoTracking()
                           .Where(x => x.Id == id)
                           .To<T>().FirstOrDefaultAsync();

            return level;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var levels = await this.levelsRepository.AllAsNoTracking()
                .OrderBy(x => x.Name)
                .To<T>().ToListAsync();

            return levels;
        }

        public async Task CreateAsync(LevelInputModel input)
        {
            var level = new Level { Name = input.Name };

            await this.levelsRepository.AddAsync(level);
            await this.levelsRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditLevelInputModel input)
        {
            var level = this.levelsRepository.All().FirstOrDefault(x => x.Id == id);
            level.Name = input.Name;

            await this.levelsRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.levelsRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}

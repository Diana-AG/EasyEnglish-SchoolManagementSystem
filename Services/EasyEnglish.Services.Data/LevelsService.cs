﻿namespace EasyEnglish.Services.Data
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

        public IEnumerable<T> GetAll<T>()
        {
            var levels = this.levelsRepository.All()
                .OrderBy(x => x.Name)
                .To<T>().ToList();

            return levels;
        }

        public async Task CreateLevelAsync(LevelInputModel input)
        {
            var level = new Level { Name = input.Name };

            await this.levelsRepository.AddAsync(level);
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

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var level = await this.levelsRepository.AllAsNoTracking()
                           .Where(x => x.Id == id)
                           .To<T>().FirstOrDefaultAsync();

            return level;
        }

        public async Task<Level> GetLevelByIdAsync(int id)
        {
            return await this.levelsRepository.All().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task DeleteAsync(int id)
        {
            var level = await this.GetLevelByIdAsync(id);
            this.levelsRepository.Delete(level);
            await this.levelsRepository.SaveChangesAsync();
        }
    }
}

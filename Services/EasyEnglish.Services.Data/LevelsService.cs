namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using EasyEnglish.Web.ViewModels.Administration.Levels;

    public class LevelsService : ILevelsService
    {
        private readonly IDeletableEntityRepository<Level> levelsRepository;

        public LevelsService(IDeletableEntityRepository<Level> levelsRepository)
        {
            this.levelsRepository = levelsRepository;
        }

        public IQueryable<LevelViewModel> AllLevels()
        {
            var levels = this.levelsRepository.All().Select(x => new LevelViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            return levels;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.levelsRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }
    }
}

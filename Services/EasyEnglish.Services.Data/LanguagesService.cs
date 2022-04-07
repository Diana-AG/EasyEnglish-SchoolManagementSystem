namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using Microsoft.EntityFrameworkCore;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languagesRepository;

        public LanguagesService(IDeletableEntityRepository<Language> languagesRepository)
        {
            this.languagesRepository = languagesRepository;
        }

        public IQueryable<LanguageViewModel> AllLanguages()
        {
            var languages = this.languagesRepository.All().Select(x => new LanguageViewModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            return languages;
        }

        public async Task CreateLanguageAsync(LanguageInputModel input)
        {
            var language = new Language { Name = input.Name };

            await this.languagesRepository.AddAsync(language);
            await this.languagesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var language = await this.GetLanguageByIdAsync(id);
            this.languagesRepository.Delete(language);
            await this.languagesRepository.SaveChangesAsync();
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.languagesRepository.All().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public async Task<Language> GetLanguageByIdAsync(int id)
        {
            return await this.languagesRepository.All().Include(x => x.Teachers).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<LanguageViewModel> GetLanguageViewModelByIdAsync(int id)
        {

            var language = await this.AllLanguages().FirstOrDefaultAsync(x => x.Id == id);

            return language;
        }
    }
}

namespace EasyEnglish.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using Microsoft.EntityFrameworkCore;

    public class LanguagesService : ILanguagesService
    {
        private readonly IDeletableEntityRepository<Language> languagesRepository;

        public LanguagesService(IDeletableEntityRepository<Language> languagesRepository)
        {
            this.languagesRepository = languagesRepository;
        }

        public async Task DeleteAsync(int id)
        {
            var language = await this.languagesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.languagesRepository.Delete(language);
            await this.languagesRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var language = await this.languagesRepository.AllAsNoTracking()
                           .Where(x => x.Id == id)
                           .To<T>().FirstOrDefaultAsync();

            return language;
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            var languages = await this.languagesRepository.All()
                .OrderBy(x => x.Name)
                .To<T>().ToListAsync();

            return languages;
        }

        public async Task CreateAsync(LanguageInputModel input)
        {
            var language = this.languagesRepository.All().FirstOrDefault(x => x.Name == input.Name);

            if (language != null)
            {
                throw new Exception($"Language with name \"{input.Name}\" already exists");
            }

            language = new Language { Name = input.Name };

            await this.languagesRepository.AddAsync(language);
            await this.languagesRepository.SaveChangesAsync();
        }

        public async Task UpdateAsync(int id, EditLanguageInputModel input)
        {
            var language = this.languagesRepository.All().FirstOrDefault(x => x.Id == id);
            language.Name = input.Name;

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
    }
}

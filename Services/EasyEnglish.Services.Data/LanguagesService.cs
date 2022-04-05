namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Languages;

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

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.languagesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.Name,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.Name));
        }

        public LanguageViewModel GetLanguageViewModelById(int? id)
        {

            var language = this.AllLanguages().FirstOrDefault(x => x.Id == id);

            return language;
        }
    }
}

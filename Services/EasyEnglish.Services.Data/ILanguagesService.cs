﻿namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Languages;

    public interface ILanguagesService
    {
        IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair();

        IEnumerable<T> GetAll<T>();

        Task CreateLanguageAsync(LanguageInputModel input);

        Task<Language> GetLanguageByIdAsync(int id);

        Task<T> GetByIdAsync<T>(int id);

        Task DeleteAsync(int id);
    }
}

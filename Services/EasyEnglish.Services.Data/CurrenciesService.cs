namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;

    public class CurrenciesService : ICurrenciesService
    {
        private readonly IDeletableEntityRepository<Currency> currenciesRepository;

        public CurrenciesService(IDeletableEntityRepository<Currency> curreniesRepository)
        {
            this.currenciesRepository = curreniesRepository;
        }

        public IEnumerable<KeyValuePair<string, string>> GetAllAsKeyValuePair()
        {
            return this.currenciesRepository.AllAsNoTracking().Select(x => new
            {
                x.Id,
                x.CurrencyCode,
            }).ToList().Select(x => new KeyValuePair<string, string>(x.Id.ToString(), x.CurrencyCode));
        }
    }
}

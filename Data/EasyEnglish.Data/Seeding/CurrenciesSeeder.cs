namespace EasyEnglish.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;

    public class CurrenciesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Currencies.Any())
            {
                return;
            }

            await dbContext.Currencies.AddAsync(new Currency { CurrencyCode = "BGN", Description = "Bulgaria Lev" });
            await dbContext.Currencies.AddAsync(new Currency { CurrencyCode = "EUR", Description = "Euro Member Countries" });
            await dbContext.Currencies.AddAsync(new Currency { CurrencyCode = "USD", Description = "United States Dollar" });
            await dbContext.Currencies.AddAsync(new Currency { CurrencyCode = "GBP", Description = "United Kingdom Pound" });
        }
    }
}

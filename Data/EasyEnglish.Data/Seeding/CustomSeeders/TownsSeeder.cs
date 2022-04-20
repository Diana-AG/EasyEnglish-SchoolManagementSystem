namespace EasyEnglish.Data.Seeding.CustomSeeders
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;

    public class TownsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (!dbContext.Countries.Any(c => c.Id == 1) || dbContext.Towns.Any())
            {
                return;
            }

            await dbContext.Towns.AddAsync(new Town { Name = "Plovdiv", CountryId = 1 });

            await dbContext.SaveChangesAsync();
        }
    }
}

namespace EasyEnglish.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;

    public class TownsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Towns.Any())
            {
                return;
            }

            await dbContext.Towns.AddAsync(new Town { Name = "Plovdiv" });

            await dbContext.SaveChangesAsync();
        }
    }
}

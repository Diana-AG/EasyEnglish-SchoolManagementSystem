namespace EasyEnglish.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    public class LanguagesSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Languages.Any())
            {
                return;
            }

            await dbContext.Languages.AddAsync(new Models.Language { Name = "English"});
            await dbContext.Languages.AddAsync(new Models.Language { Name = "German"});
            await dbContext.Languages.AddAsync(new Models.Language { Name = "French"});
            await dbContext.Languages.AddAsync(new Models.Language { Name = "Russian" });

            await dbContext.SaveChangesAsync();
        }
    }
}

namespace EasyEnglish.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;

    public class LevelsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Levels.Any())
            {
                return;
            }

            await dbContext.Levels.AddAsync(new Level { Name = "A1-Beginner" });
            await dbContext.Levels.AddAsync(new Level { Name = "A2-Elementary" });
            await dbContext.Levels.AddAsync(new Level { Name = "B1-Intermediate" });
            await dbContext.Levels.AddAsync(new Level { Name = "B2-Upper Intermediate" });
            await dbContext.Levels.AddAsync(new Level { Name = "C1-Advanced" });
            await dbContext.Levels.AddAsync(new Level { Name = "Grade 1" });
            await dbContext.Levels.AddAsync(new Level { Name = "Grade 2" });
            await dbContext.Levels.AddAsync(new Level { Name = "Grade 3" });
            await dbContext.Levels.AddAsync(new Level { Name = "Grade 4" });

            await dbContext.SaveChangesAsync();
        }
    }
}

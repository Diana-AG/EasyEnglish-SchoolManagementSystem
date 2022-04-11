﻿namespace EasyEnglish.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;

    public class TrainingFormsSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.TrainingForms.Any())
            {
                return;
            }

            await dbContext.TrainingForms.AddAsync(new TrainingForm { Name = "Once per week" });
            await dbContext.TrainingForms.AddAsync(new TrainingForm { Name = "Twice per week" });
            await dbContext.TrainingForms.AddAsync(new TrainingForm { Name = "Individual" });

            await dbContext.SaveChangesAsync();
        }
    }
}

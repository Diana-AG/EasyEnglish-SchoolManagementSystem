﻿namespace EasyEnglish.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Data.Repositories;
    using EasyEnglish.Web.ViewModels.Administration.TrainingForms;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class TrainingFormsServiceTests
    {
        [Fact]
        public async Task CreateAsyncMethodShouldAddTrainingFormInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TrainingFormsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.TrainingForms.Add(new TrainingForm { Name = "TrainingForm 1" });
            dbContext.TrainingForms.Add(new TrainingForm { Name = "TrainingForm 2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<TrainingForm>(dbContext);
            var service = new TrainingFormsService(repository);

            await service.CreateAsync(new TrainingFormInputModel
            {
                Name = "TrainingForm 3",
            });

            Assert.Equal(3, repository.All().Count());
        }

        [Fact]
        public async Task UpdateAsyncTrainingFormUpdatesData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("TrainingFormsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.TrainingForms.Add(new TrainingForm { Id = 1, Name = "TrainingForm 1" });
            dbContext.TrainingForms.Add(new TrainingForm { Id = 2, Name = "TrainingForm 2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<TrainingForm>(dbContext);
            var service = new TrainingFormsService(repository);

            var model = new EditTrainingFormInputModel
            {
                Id = 1,
                Name = "New TrainingForm",
            };

            await service.UpdateAsync(model.Id, model);

            Assert.Equal("New TrainingForm", model.Name);
        }
    }
}

namespace EasyEnglish.Services.Data.Tests
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Data.Repositories;
    using EasyEnglish.Web.ViewModels.Administration.Levels;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class LevelsServiceTests
    {
        [Fact]
        public async Task CreateAsyncMethodShouldAddLevelInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("LevelsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Levels.Add(new Level { Name = "Level 1" });
            dbContext.Levels.Add(new Level { Name = "Level 2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Level>(dbContext);
            var service = new LevelsService(repository);

            await service.CreateAsync(new LevelInputModel
            {
                Name = "Level 3",
            });

            Assert.Equal(3, repository.All().Count());
        }

        [Fact]
        public async Task UpdateAsyncLevelReturnsCorrectData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("LevelsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Levels.Add(new Level { Id = 1, Name = "Level 1" });
            dbContext.Levels.Add(new Level { Id = 2, Name = "Level 2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Level>(dbContext);
            var service = new LevelsService(repository);

            var model = new EditLevelInputModel
            {
                Id = 1,
                Name = "New level name",
            };

            await service.UpdateAsync(1, model);

            var dataForEdit = await service.GetByIdAsync<LevelViewModel>(1);

            await service.UpdateAsync(1, model);

            var editedData = await service.GetByIdAsync<LevelViewModel>(1);

            Assert.Equal("New level name", editedData.Name);
        }


        [Fact]
        public async Task GetByIdAsyncReturnsCorrectLevel()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("LevelsTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Levels.Add(new Level { Id = 1, Name = "Level 1" });
            dbContext.Levels.Add(new Level { Id = 2, Name = "Level 2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Level>(dbContext);
            var service = new LevelsService(repository);

            var level = await service.GetByIdAsync<LevelViewModel>(1);

            Assert.NotNull(level);
            Assert.Equal("Level 1", level.Name);
        }
    }
}

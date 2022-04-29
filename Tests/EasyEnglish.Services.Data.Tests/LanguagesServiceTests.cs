namespace EasyEnglish.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Data.Repositories;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class LanguagesServiceTests
    {
        [Fact]
        public async Task CreateAsyncMethodShouldAddLanguageInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("LanguagesTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Languages.Add(new Language { Name = "Language 1" });
            dbContext.Languages.Add(new Language { Name = "Language 2" });
            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Language>(dbContext);
            var service = new LanguagesService(repository);

            await service.CreateAsync(new LanguageInputModel
            {
                Name = "Language 3",
            });

            //var editedData = service.GetAllAsync<LanguageViewModel>();
            Assert.Equal(3, repository.All().Count());
        }
    }
}

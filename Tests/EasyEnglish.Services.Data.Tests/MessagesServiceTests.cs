namespace EasyEnglish.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Data.Repositories;
    using EasyEnglish.Web.ViewModels.Administration.Messages;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class MessagesServiceTests
    {
        [Fact]
        public async Task AddAsyncMethodShouldAddMessageInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("MessagesTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Messages.Add(new Message
            {
                Description = "Message description 1",
                StartDate = DateTime.UtcNow,
                EndDate = null,
            });

            dbContext.Messages.Add(new Message
            {
                Description = "Message description 2",
                StartDate = DateTime.UtcNow,
                EndDate = null,
            });

            await dbContext.SaveChangesAsync();

            using var repository = new EfDeletableEntityRepository<Message>(dbContext);
            var service = new MessagesService(repository);

            await service.AddAsync(
                new MessageInputModel
                {
                    Description = "Message description 3",
                    StartDate = DateTime.UtcNow,
                    EndDate = null,
                },
                Guid.NewGuid().ToString());

            Assert.Equal(3, repository.All().Count());
        }
    }
}

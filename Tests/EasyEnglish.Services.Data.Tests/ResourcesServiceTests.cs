namespace EasyEnglish.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using EasyEnglish.Data;
    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Data.Repositories;
    using Microsoft.EntityFrameworkCore;
    using Moq;
    using Xunit;

    public class ResourcesServiceTests
    {
        [Fact]
        public void AddRemoteUrlAsyncMethodShouldAddResourceUrlInDatabase()
        {
            var resourcesRepository = new Mock<IDeletableEntityRepository<Resource>>();
            var courseTypesRepository = new Mock<IDeletableEntityRepository<CourseType>>();
            resourcesRepository.Setup(r => r.All()).Returns(new List<Resource>
                                                        {
                                                            new Resource(),
                                                            new Resource(),
                                                            new Resource(),
                                                        }.AsQueryable());

            courseTypesRepository.Setup(r => r.All()).Returns(new List<CourseType>
                                                        {
                                                            new CourseType(),
                                                        }.AsQueryable());

            var service = new ResourcesService(resourcesRepository.Object, courseTypesRepository.Object);

            Assert.Equal(3, service.GetCount());
            resourcesRepository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void UploadFileAsyncShouldAddResourceInDatabase()
        {
            var resourcesRepository = new Mock<IDeletableEntityRepository<Resource>>();
            var courseTypesRepository = new Mock<IDeletableEntityRepository<CourseType>>();
            resourcesRepository.Setup(r => r.All()).Returns(new List<Resource>
                                                        {
                                                            new Resource(),
                                                            new Resource(),
                                                            new Resource(),
                                                        }.AsQueryable());

            courseTypesRepository.Setup(r => r.All()).Returns(new List<CourseType>
                                                        {
                                                            new CourseType(),
                                                        }.AsQueryable());

            var service = new ResourcesService(resourcesRepository.Object, courseTypesRepository.Object);

            Assert.Equal(3, service.GetCount());
            resourcesRepository.Verify(x => x.All(), Times.Once);
        }

        [Fact]
        public void NameExistsMethodShouldReturnsTrueIfResourceWithTHisNameAlreadyExistInDatabase()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("ResourcesTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Resources.Add(new Resource { Name = "Resource 1" });
            dbContext.Resources.Add(new Resource { Name = "Resource 2" });
            dbContext.CourseTypes.Add(new CourseType { LanguageId = 1, LevelId = 1 });
            dbContext.SaveChanges();

            using var resourcesRepository = new EfDeletableEntityRepository<Resource>(dbContext);
            using var courseTypesRepository = new EfDeletableEntityRepository<CourseType>(dbContext);

            var service = new ResourcesService(resourcesRepository, courseTypesRepository);

            var expected = service.NameExists("Resource 1");

            Assert.True(expected);
        }
    }
}

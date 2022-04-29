namespace EasyEnglish.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using Moq;
    using Xunit;

    public class CoursesServiceTests
    {
        [Fact]
        public void GetCountShouldReturnCorrectNumber()
        {
            var coursesRepository = new Mock<IDeletableEntityRepository<Course>>();
            var usersRepository = new Mock<IDeletableEntityRepository<ApplicationUser>>();
            coursesRepository.Setup(r => r.All()).Returns(new List<Course>
                                                        {
                                                            new Course(),
                                                            new Course(),
                                                            new Course(),
                                                        }.AsQueryable());

            usersRepository.Setup(r => r.All()).Returns(new List<ApplicationUser>
                                                        {
                                                            new ApplicationUser(),
                                                            new ApplicationUser(),
                                                            new ApplicationUser(),
                                                        }.AsQueryable());

            var service = new CoursesService(coursesRepository.Object, usersRepository.Object);
            Assert.Equal(3, service.GetCount());
            coursesRepository.Verify(x => x.All(), Times.Once);
        }
    }
}

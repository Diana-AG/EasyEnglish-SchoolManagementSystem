namespace EasyEnglish.Services.Data.Tests
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using EasyEnglish.Data;
    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Data.Repositories;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using Microsoft.EntityFrameworkCore;
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

        [Fact]
        public async Task UpdateAsyncCourseUpdatesDescriptionData()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("CoursesTestDb").Options;
            using var dbContext = new ApplicationDbContext(options);
            dbContext.Courses.Add(new Course { Id = 1, Description = "Course Description 1" });
            dbContext.Courses.Add(new Course { Id = 2, Description = "Course Description 2" });
            await dbContext.SaveChangesAsync();

            using var courseRepository = new EfDeletableEntityRepository<Course>(dbContext);
            using var usersRepository = new EfDeletableEntityRepository<ApplicationUser>(dbContext);
            var service = new CoursesService(courseRepository, usersRepository);

            var model = new EditCourseInputModel
            {
                Id = 1,
                CourseTypeId = 1,
                TrainingFormId = 1,
                Description = "New Course Description",
                StartDate = System.DateTime.UtcNow,
            };

            await service.UpdateAsync(model.Id, model);

            Assert.Equal("New Course Description", model.Description);
        }
    }
}

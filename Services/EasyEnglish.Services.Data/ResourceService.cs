namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Courses;

    public class ResourceService : IResourceService
    {
        private readonly IDeletableEntityRepository<Resource> resourcesRespository;
        private readonly IDeletableEntityRepository<CourseType> courseTypesRespository;

        public ResourceService(
            IDeletableEntityRepository<Resource> resourcesRespository,
            IDeletableEntityRepository<CourseType> courseTypesRespository)
        {
            this.resourcesRespository = resourcesRespository;
            this.courseTypesRespository = courseTypesRespository;
        }

        public async Task CreateAsync(CreateResourceInputModel input)
        {
            var resource = new Resource
            {
                Description = input.Description,
                Url = input.Url,
            };

            foreach (var inputCourseTypes in input.CourseTypes)
            {
                var courseType = this.courseTypesRespository.All().FirstOrDefault(x => x.Description == inputCourseTypes.Description);

                //Add logic when courseType doesnt exist
                resource.CourseTypes.Add(courseType);
            }

            await this.resourcesRespository.AddAsync(resource);
            await this.resourcesRespository.SaveChangesAsync();
        }
    }
}

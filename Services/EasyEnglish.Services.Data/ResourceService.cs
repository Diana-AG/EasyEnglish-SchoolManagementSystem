namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Resources;
    using Microsoft.EntityFrameworkCore;

    public class ResourceService : IResourceService
    {
        private readonly IDeletableEntityRepository<Resource> resourcesRepository;
        private readonly IDeletableEntityRepository<CourseType> courseTypesRespository;

        public ResourceService(
            IDeletableEntityRepository<Resource> resourcesRepository,
            IDeletableEntityRepository<CourseType> courseTypesRespository)
        {
            this.resourcesRepository = resourcesRepository;
            this.courseTypesRespository = courseTypesRespository;
        }

        public IQueryable<ResourceViewModel> AllResources()
        {
            var resources = this.resourcesRepository.All()
                .Include(x => x.CourseTypes)
                .Select(x => new ResourceViewModel
                {
                    Id = x.Id,
                    Description = x.Description,
                    Url = x.Url,
                });

            return resources;
        }

        public async Task CreateAsync(ResourceInputModel input)
        {
            var resource = new Resource
            {
                Description = input.Description,
                Url = input.Url,
            };

            //foreach (var inputCourseTypes in input.CourseTypes)
            //{
            //    var courseType = this.courseTypesRespository.All().FirstOrDefault(x => x.Description == inputCourseTypes.Description);

            //    //Add logic when courseType doesnt exist
            //    resource.CourseTypes.Add(new ResourceCourseType { ResourceId = resource.Id, CourseTypeId = courseType.Id });
            //}

            await this.resourcesRepository.AddAsync(resource);
            await this.resourcesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await this.GetResourceByIdAsync(id);
            this.resourcesRepository.Delete(resource);
            await this.resourcesRepository.SaveChangesAsync();
        }

        public async Task<Resource> GetResourceByIdAsync(int id)
        {
            return await this.resourcesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ResourceViewModel> GetResourceViewModelByIdAsync(int id)
        {
            return await this.AllResources().FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

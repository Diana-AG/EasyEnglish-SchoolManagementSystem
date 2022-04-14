namespace EasyEnglish.Services.Data
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.Resources;
    using Microsoft.EntityFrameworkCore;

    public class ResourceService : IResourceService
    {
        private readonly string[] allowedExtensions = new[] { "pdf", "doc", "docx", "docm", "ppt", "pptx", "jpg", "png" };
        private readonly IDeletableEntityRepository<Resource> resourcesRepository;
        private readonly IDeletableEntityRepository<CourseType> courseTypesRespository;

        public ResourceService(
            IDeletableEntityRepository<Resource> resourcesRepository,
            IDeletableEntityRepository<CourseType> courseTypesRespository)
        {
            this.resourcesRepository = resourcesRepository;
            this.courseTypesRespository = courseTypesRespository;
        }

        public ResourceViewModel AllResources()
        {
            var urlResources = this.resourcesRepository.All()
                .Include(x => x.CourseTypes)
                .Where(x => x.Url != null)
                .OrderBy(x => x.Name)
                .Select(x => new ResourceUrlViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                    Url = x.Url,
                }).ToList();

            var fileResources = this.resourcesRepository.All()
                .Include(x => x.CourseTypes)
                .Where(x => x.Url == null)
                .OrderBy(x => x.Name)
                .Select(x => new ResourceFileViewModel
                {
                    Id = x.Id,
                    Name = x.Name,
                }).ToList();

            var resources = new ResourceViewModel
            {
                ResourceUrls = urlResources,
                ResourceFiles = fileResources,
            };

            return resources;
        }

        public async Task CreateAsync(ResourceInputModel input, string userId, string imagePath)
        {
            var resource = new Resource
            {
                Name = input.Name,
                Url = input.Url,
            };

            // foreach (var inputCourseTypes in input.CourseTypes)
            // {
            //    var courseType = this.courseTypesRespository.All().FirstOrDefault(x => x.Description == inputCourseTypes.Description);

            // //Add logic when courseType doesnt exist
            //    resource.CourseTypes.Add(new ResourceCourseType { ResourceId = resource.Id, CourseTypeId = courseType.Id });
            // }

            // /wwwroot/images/recipes/jhdsi-343g3h453-=g34g.jpg
            if (input.Images != null)
            {
                Directory.CreateDirectory($"{imagePath}/resources/");
                foreach (var image in input.Images)
                {
                    var extension = Path.GetExtension(image.FileName).TrimStart('.');
                    if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
                    {
                        throw new Exception($"Invalid image extension {extension}");
                    }

                    var dbImage = new Image
                    {
                        UserId = userId,
                        ResourceId = resource.Id,
                        Extension = extension,
                    };
                    resource.Images.Add(dbImage);

                    var physicalPath = $"{imagePath}/resources/{dbImage.Id}.{extension}";
                    using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
                    await image.CopyToAsync(fileStream);
                }
            }

            await this.resourcesRepository.AddAsync(resource);
            await this.resourcesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await this.resourcesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.resourcesRepository.Delete(resource);
            await this.resourcesRepository.SaveChangesAsync();
        }

        public async Task<T> GetByIdAsync<T>(int id)
        {
            var resource = await this.resourcesRepository.AllAsNoTracking()
                .Where(x => x.Id == id)
                .To<T>().FirstOrDefaultAsync();

            return resource;
        }

        public int GetCount()
        {
            return this.resourcesRepository.All().Count();
        }
    }
}

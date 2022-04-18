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
        private readonly IDeletableEntityRepository<Image> imagesRespository;
        private readonly IDeletableEntityRepository<Resource> resourcesRepository;
        private readonly IDeletableEntityRepository<CourseType> courseTypesRespository;

        public ResourceService(
            IDeletableEntityRepository<Image> imagesRespository,
            IDeletableEntityRepository<Resource> resourcesRepository,
            IDeletableEntityRepository<CourseType> courseTypesRespository)
        {
            this.imagesRespository = imagesRespository;
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

        public async Task AddRemoteUrlAsync(ResourceUrlInputModel input)
        {
            var resource = new Resource
            {
                Name = input.Name,
                Url = input.Url,
            };

            resource.CourseTypes.Add(new ResourceCourseType { ResourceId = resource.Id, CourseTypeId = input.CourseTypeId });

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

        public async Task UploadFileAsync(ResourceUploadFileInputModel input, string resourcePath)
        {
            var extension = Path.GetExtension(input.Image.FileName).TrimStart('.');
            if (!this.allowedExtensions.Any(x => extension.EndsWith(x)))
            {
                throw new Exception($"Invalid file extension {extension}");
            }

            var resource = new Resource
            {
                Name = input.Name,
                Extension = extension,
                ContentType = input.Image.ContentType,
            };

            resource.CourseTypes.Add(new ResourceCourseType
            {
                ResourceId = resource.Id,
                CourseTypeId = input.CourseTypeId,
            });

            await this.resourcesRepository.AddAsync(resource);
            await this.resourcesRepository.SaveChangesAsync();

            Directory.CreateDirectory($"{resourcePath}");
            var physicalPath = $"{resourcePath}/{resource.Id}.{extension}";
            using Stream fileStream = new FileStream(physicalPath, FileMode.Create);
            await input.Image.CopyToAsync(fileStream);
        }
    }
}

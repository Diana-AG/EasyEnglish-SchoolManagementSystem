namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using CloudinaryDotNet;
    using CloudinaryDotNet.Actions;
    using EasyEnglish.Common;
    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.Controllers;
    using EasyEnglish.Web.ViewModels.Administration.Resources;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    [Area("Administration")]
    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.ManagerRoleName}, {GlobalConstants.TeacherRoleName} ")]
    public class ResourcesController : BaseController
    {
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;
        private readonly IDeletableEntityRepository<Data.Models.Resource> dataRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IResourcesService resourcesService;
        private readonly ICourseTypesService courseTypesService;
        private readonly Cloudinary cloudinary;

        public ResourcesController(
            IDeletableEntityRepository<CourseType> courseTypesRepository,
            IDeletableEntityRepository<Data.Models.Resource> dataRepository,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IResourcesService resourcesService,
            ICourseTypesService courseTypesService,
            Cloudinary cloudinary)
        {
            this.courseTypesRepository = courseTypesRepository;
            this.dataRepository = dataRepository;
            this.userManager = userManager;
            this.environment = environment;
            this.resourcesService = resourcesService;
            this.courseTypesService = courseTypesService;
            this.cloudinary = cloudinary;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = this.resourcesService.GetAll();

            return this.View(viewModel);
        }

        public IActionResult AddRemoteUrl()
        {
            var viewModel = new ResourceUrlInputModel();
            viewModel.CourseTypeItems = this.courseTypesService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddRemoteUrl(ResourceUrlInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseTypeItems = this.courseTypesService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            await this.resourcesService.AddRemoteUrlAsync(input);

            this.ViewData[MessageConstant.SuccessMessage] = "Resource added successfully.";

            return this.RedirectToAction(nameof(this.Index));
        }

        public IActionResult UploadFile()
        {
            var viewModel = new ResourceUploadFileInputModel();
            viewModel.CourseTypeItems = this.courseTypesService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> UploadFile(ResourceUploadFileInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseTypeItems = this.courseTypesService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            try
            {
                await this.resourcesService.UploadFileAsync(input, $"{this.environment.WebRootPath}/resources");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CourseTypeItems = this.courseTypesService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            this.ViewData[MessageConstant.SuccessMessage] = "Resource added successfully.";

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Upload()
        {
            var uploadParams = new ImageUploadParams()
            {
                File = new FileDescription(@"D:\!Dee folder\NoraFoxx\51.jpg"),
            };
            var uploadResult = await this.cloudinary.UploadAsync(uploadParams);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public FileResult Download(int id)
        {
            var resource = this.dataRepository.All().FirstOrDefault(x => x.Id == id);

            string fileName = $"{resource.Id}.{resource.Extension}";
            string phisicalPath = $"{this.environment.WebRootPath}/resources/{fileName}";
            string contentType = resource.ContentType;
            return this.PhysicalFile(phisicalPath, contentType, $"{resource.Name}.{resource.Extension}");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            string phisicalPath = $"{this.environment.WebRootPath}/resources";

            await this.resourcesService.DeleteAsync(id, phisicalPath);
            return this.RedirectToAction(nameof(this.Index));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyName(string name)
        {
            if (this.resourcesService.NameExists(name))
            {
                return this.Json($"Resource {name} already exists.");
            }

            return this.Json(true);
        }
    }
}

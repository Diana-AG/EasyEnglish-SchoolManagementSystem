namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.ViewModels.Administration.Resources;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.StaticFiles;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ResourcesController : AdministratorController
    {
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;
        private readonly IDeletableEntityRepository<Resource> dataRepository;
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IWebHostEnvironment environment;
        private readonly IResourceService resourceService;
        private readonly ICourseTypeService courseTypeService;

        public ResourcesController(
            IDeletableEntityRepository<CourseType> courseTypesRepository,
            IDeletableEntityRepository<Resource> dataRepository,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment environment,
            IResourceService resourceService,
            ICourseTypeService courseTypeService)
        {
            this.courseTypesRepository = courseTypesRepository;
            this.dataRepository = dataRepository;
            this.userManager = userManager;
            this.environment = environment;
            this.resourceService = resourceService;
            this.courseTypeService = courseTypeService;
        }

        // GET: Administration/Resources
        public async Task<IActionResult> Index()
        {
            var viewModel = this.resourceService.GetAll();

            return this.View(viewModel);
        }

        // GET: Administration/Resources/Create
        public IActionResult AddRemoteUrl()
        {
            var viewModel = new ResourceUrlInputModel();
            viewModel.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }

        // POST: Administration/Resources/Create
        [HttpPost]
        public async Task<IActionResult> AddRemoteUrl(ResourceUrlInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            await this.resourceService.AddRemoteUrlAsync(input);

            this.ViewData[MessageConstant.SuccessMessage] = "Resource added successfully.";

            // TODO: Redirect to resource details page
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/Resources/Create
        public IActionResult UploadFile()
        {
            var viewModel = new ResourceUploadFileInputModel();
            viewModel.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }

        // POST: Administration/Resources/Create
        [HttpPost]
        public async Task<IActionResult> UploadFile(ResourceUploadFileInputModel input)
        {

            if (!this.ModelState.IsValid)
            {
                input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            try
            {
                await this.resourceService.UploadFileAsync(input, $"{this.environment.WebRootPath}/resources");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            this.ViewData[MessageConstant.SuccessMessage] = "Resource added successfully.";

            // TODO: Redirect to resource details page
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

        // POST: Administration/Resources/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await this.resourceService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        [AcceptVerbs("GET", "POST")]
        public IActionResult VerifyName(string name)
        {
            if (this.resourceService.NameExists(name))
            {
                return this.Json($"Resource {name} already exists.");
            }

            return this.Json(true);
        }
    }
}

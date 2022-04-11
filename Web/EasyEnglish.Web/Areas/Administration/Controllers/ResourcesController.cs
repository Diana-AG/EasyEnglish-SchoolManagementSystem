namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Administration.Resources;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ResourcesController : AdministratorController
    {
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;
        private readonly IDeletableEntityRepository<Resource> dataRepository;
        private readonly IResourceService resourceService;
        private readonly ICourseTypeService courseTypeService;

        public ResourcesController(
            IDeletableEntityRepository<CourseType> courseTypesRepository,
            IDeletableEntityRepository<Resource> dataRepository,
            IResourceService resourceService,
            ICourseTypeService courseTypeService)
        {
            this.courseTypesRepository = courseTypesRepository;
            this.dataRepository = dataRepository;
            this.resourceService = resourceService;
            this.courseTypeService = courseTypeService;
        }

        // GET: Administration/Resources
        public async Task<IActionResult> Index()
        {
            var viewModels = await this.dataRepository.All()
                .Select(x => new ResourceViewModel
                {
                    Id = x.Id,
                    Description = x.Name,
                    Url = x.Url,
                }).ToListAsync();

            return this.View(viewModels);
        }

        // GET: Administration/Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resource = await this.resourceService.GetResourceByIdAsync((int)id);
            if (resource == null)
            {
                return this.NotFound();
            }

            return this.View(resource);
        }

        // GET: Administration/Resources/Create
        public IActionResult Create()
        {
            var input = new ResourceInputModel();
            input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();

            return this.View(input);
        }

        // POST: Administration/Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ResourceInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.resourceService.CreateAsync(input);

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(input);
        }

        // GET: Administration/Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resource = await this.dataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (resource == null)
            {
                return this.NotFound();
            }

            return this.View(resource);
        }

        // POST: Administration/Resources/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,Url,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Resource resource)
        {
            if (id != resource.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(resource);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ResourceExists(resource.Id))
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(resource);
        }

        // GET: Administration/Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resourceViewModel = await this.resourceService.GetResourceViewModelByIdAsync((int)id);
            if (resourceViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(resourceViewModel);
        }

        // POST: Administration/Resources/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.resourceService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ResourceExists(int id)
        {
            return this.dataRepository.All().Any(x => x.Id == id);
        }
    }
}

namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Areas.Administration.ViewModels;
    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CourseTypesController : AdministratorController
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;
        private readonly ICourseTypeService courseTypeService;
        private readonly ILanguagesService languagesService;
        private readonly ILevelsService levelsService;

        public CourseTypesController(
            ApplicationDbContext context,
            IDeletableEntityRepository<CourseType> courseTypesRepository,
            ICourseTypeService courseTypeService,
            ILanguagesService languagesService,
            ILevelsService levelsService)
        {
            this.dbContext = context;
            this.courseTypesRepository = courseTypesRepository;
            this.courseTypeService = courseTypeService;
            this.languagesService = languagesService;
            this.levelsService = levelsService;
        }

        // GET: Administration/CourseTypes
        public async Task<IActionResult> Index()
        {
            var courseTypeViewModels = this.courseTypeService.AllCourseTypes();

            return this.View(await courseTypeViewModels.ToListAsync());
        }

        // GET: Administration/CourseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseType = await this.courseTypeService.GetCourseTypeViewModelByIdAsync((int)id);
            if (courseType == null)
            {
                return this.NotFound();
            }

            return this.View(courseType);
        }

        // GET: Administration/CourseTypes/Create
        public IActionResult Create()
        {
            var input = new CourseTypeInputModel();

            input.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
            input.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
            return this.View(input);
        }

        // POST: Administration/CourseTypes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseTypeInputModel input)
        {
            var language = await this.languagesService.GetByIdAsync<LanguageViewModel>(input.LanguageId);
            var level = await this.levelsService.GetLevelByIdAsync(input.LevelId);
            if (language == null || level == null)
            {
                return this.NotFound();
            }

            // if (this.courseTypeService.CourseTypeExists(input.LanguageId, input.LevelId))
            // {
            //    //return message "Course Type with this language and this level already exist" redirect Details/courseTypeId
            //    input.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
            //    input.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
            //    return this.View(input);
            // }
            if (this.ModelState.IsValid && !this.courseTypeService.CourseTypeExists(input.LanguageId, input.LevelId))
            {
                await this.courseTypeService.CreateCourseAsync(input);
                return this.RedirectToAction(nameof(this.Index));
            }

            input.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
            input.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
            return this.View(input);
        }

        // GET: Administration/CourseTypes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseType = await this.dbContext.CourseTypes.FindAsync(id);
            if (courseType == null)
            {
                return this.NotFound();
            }

            this.ViewData["LanguageId"] = new SelectList(this.dbContext.Languages, "Id", "Name", courseType.LanguageId);
            this.ViewData["LevelId"] = new SelectList(this.dbContext.Levels, "Id", "Name", courseType.LevelId);
            return this.View(courseType);
        }

        // GET: Administration/CourseTypes/AddResource/5
        public async Task<IActionResult> AddResource(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resources = await this.dbContext.Resources.ToListAsync();

            var courseTypeResouce = new CourseTypeResourseViewModel
            {
                CourseId = (int)id,
                Resources = resources,
            };

            return this.View(courseTypeResouce);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddResource(CourseTypereSourseInputModel model)
        {
            if (model.Id == null || model.CourseId == null)
            {
                return this.NotFound();
            }

            var courseType = await this.dbContext.CourseTypes.FindAsync(model.CourseId);
            var resource = await this.dbContext.Resources.FindAsync(model.Id);
            if (courseType == null || resource == null)
            {
                return this.NotFound();
            }

            courseType.Resources.Add(new ResourceCourseType { ResourceId = resource.Id, CourseTypeId = courseType.Id });
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
        }

        // POST: Administration/CourseTypes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LanguageId,LevelId,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] CourseType courseType)
        {
            if (id != courseType.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dbContext.Update(courseType);
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CourseTypeExists(courseType.Id))
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

            this.ViewData["LanguageId"] = new SelectList(this.dbContext.Languages, "Id", "Name", courseType.LanguageId);
            this.ViewData["LevelId"] = new SelectList(this.dbContext.Levels, "Id", "Name", courseType.LevelId);
            return this.View(courseType);
        }

        // GET: Administration/CourseTypes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseType = await this.courseTypeService.GetCourseTypeViewModelByIdAsync((int)id);
            if (courseType == null)
            {
                return this.NotFound();
            }

            return this.View(courseType);
        }

        // POST: Administration/CourseTypes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.courseTypeService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CourseTypeExists(int id)
        {
            return this.dbContext.CourseTypes.Any(e => e.Id == id);
        }
    }
}

namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Areas.Administration.ViewModels;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.ViewModels.Administration.CourseTypes;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using EasyEnglish.Web.ViewModels.Administration.Levels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CourseTypesController : AdministratorController
    {
        private readonly ICourseTypesService courseTypeService;
        private readonly ILanguagesService languagesService;
        private readonly ILevelsService levelsService;

        public CourseTypesController(
            ICourseTypesService courseTypeService,
            ILanguagesService languagesService,
            ILevelsService levelsService)
        {
            this.courseTypeService = courseTypeService;
            this.languagesService = languagesService;
            this.levelsService = levelsService;
        }

        // GET: Administration/CourseTypes
        public async Task<IActionResult> Index()
        {
            var courseTypeViewModels = await this.courseTypeService.GetAllAsync<CourseTypeViewModel>();

            return this.View(courseTypeViewModels);
        }

        // GET: Administration/CourseTypes/Create
        public IActionResult Create()
        {
            var input = new CourseTypeInputModel
            {
                LevelsItems = this.levelsService.GetAllAsKeyValuePair(),
                LanguagesItems = this.languagesService.GetAllAsKeyValuePair(),
            };

            return this.View(input);
        }

        // POST: Administration/CourseTypes/Create
        [HttpPost]
        public async Task<IActionResult> Create(CourseTypeInputModel input)
        {
            var language = await this.languagesService.GetByIdAsync<LanguageViewModel>(input.LanguageId);
            var level = await this.levelsService.GetByIdAsync<LevelViewModel>(input.LevelId);
            if (language == null || level == null)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                input.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
                input.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            await this.courseTypeService.CreateAsync(input);
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/CourseTypes/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var courseType = await this.courseTypeService.GetByIdAsync<CourseTypeViewModel>(id);
            if (courseType == null)
            {
                return this.NotFound();
            }

            return this.View(courseType);
        }

        // GET: Administration/CourseTypes/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var courseType = await this.courseTypeService.GetByIdAsync<EditCourseTypeInputModel>(id);
            if (courseType == null)
            {
                return this.NotFound();
            }

            courseType.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
            courseType.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
            return this.View(courseType);
        }

        // POST: Administration/CourseTypes/Edit/5
        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditCourseTypeInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                input.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
                input.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            await this.courseTypeService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { Id = id });
        }

        // GET: Administration/CourseTypes/AddResource/5
        //public async Task<IActionResult> AddResource(int id)
        //{
        //    var resources = await this.dbContext.Resources.ToListAsync();

        //    var courseTypeResouce = new CourseTypeResourseViewModel
        //    {
        //        CourseId = id,
        //        Resources = resources,
        //    };

        //    return this.View(courseTypeResouce);
        //}

        //[HttpPost]
        //public async Task<IActionResult> AddResource(CourseTypeResourseInputModel model)
        //{
        //    if (model.Id == null || model.CourseId == null)
        //    {
        //        return this.NotFound();
        //    }

        //    var courseType = await this.dbContext.CourseTypes.FindAsync(model.CourseId);
        //    var resource = await this.dbContext.Resources.FindAsync(model.Id);
        //    if (courseType == null || resource == null)
        //    {
        //        return this.NotFound();
        //    }

        //    courseType.Resources.Add(new ResourceCourseType { ResourceId = resource.Id, CourseTypeId = courseType.Id });
        //    await this.dbContext.SaveChangesAsync();

        //    return this.RedirectToAction(nameof(this.Index));
        //}

        // GET: Administration/CourseTypes/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var courseType = await this.courseTypeService.GetByIdAsync<CourseTypeViewModel>(id);
            if (courseType == null)
            {
                return this.NotFound();
            }

            this.ViewData[MessageConstant.WarningMessage] = "Please, be careful! Deletion may result in data loss";

            return this.View(courseType);
        }

        // POST: Administration/CourseTypes/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.courseTypeService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

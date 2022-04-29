namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.ViewModels.Administration.Levels;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class LevelsController : AdministratorController
    {
        private readonly ILevelsService levelsService;

        public LevelsController(
            ILevelsService levelsService)
        {
            this.levelsService = levelsService;
        }

        public async Task<IActionResult> Index()
        {
            var levelViewModels = await this.levelsService.GetAllAsync<LevelViewModel>();

            return this.View(levelViewModels);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LevelInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.levelsService.CreateAsync(input);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var level = await this.levelsService.GetByIdAsync<EditLevelInputModel>(id);
            if (level == null)
            {
                return this.NotFound();
            }

            return this.View(level);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditLevelInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.levelsService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var level = await this.levelsService.GetByIdAsync<LevelViewModel>(id);
            if (level == null)
            {
                return this.NotFound();
            }

            this.ViewData[MessageConstant.WarningMessage] = "Please, be careful! Deletion may result in data loss";

            return this.View(level);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.levelsService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

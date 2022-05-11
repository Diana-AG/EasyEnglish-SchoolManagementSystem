namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class LanguagesController : AdministratorController
    {
        private readonly ILanguagesService languagesService;

        public LanguagesController(
            ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        public async Task<IActionResult> Index()
        {
            var languageViewModels = await this.languagesService.GetAllAsync<LanguageViewModel>();

            return this.View(languageViewModels);
        }

        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(LanguageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.languagesService.CreateAsync(input);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var language = await this.languagesService.GetByIdAsync<EditLanguageInputModel>(id);
            if (language == null)
            {
                return this.NotFound();
            }

            return this.View(language);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditLanguageInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.languagesService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var language = await this.languagesService.GetByIdAsync<LanguageViewModel>(id);
            if (language == null)
            {
                return this.NotFound();
            }

            this.ViewData[MessageConstant.WarningMessage] = "Please, be careful! Deletion may result in data loss";

            return this.View(language);
        }

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.languagesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

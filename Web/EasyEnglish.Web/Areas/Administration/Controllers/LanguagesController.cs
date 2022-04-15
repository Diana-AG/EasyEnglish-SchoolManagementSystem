﻿namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class LanguagesController : AdministratorController
    {
        private readonly ILanguagesService languagesService;

        public LanguagesController(
            ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        // GET: Administration/Languages
        public async Task<IActionResult> Index()
        {
            var languageViewModels = await this.languagesService.GetAllAsync<LanguageViewModel>();

            return this.View(languageViewModels);
        }

        // GET: Administration/Languages/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Languages/Create
        [HttpPost]
        public async Task<IActionResult> Create(LanguageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            await this.languagesService.CreateAsync(input);
            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/Languages/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var language = await this.languagesService.GetByIdAsync<EditLanguageInputModel>(id);
            if (language == null)
            {
                return this.NotFound();
            }

            return this.View(language);
        }

        // POST: Administration/Languages/Edit/5
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

        // GET: Administration/Languages/Delete/5
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

        // POST: Administration/Languages/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.languagesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

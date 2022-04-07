namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Administration.Languages;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class LanguagesController : AdministratorController
    {
        private readonly IDeletableEntityRepository<Language> dataRepository;
        private readonly ILanguagesService languagesService;

        public LanguagesController(
            IDeletableEntityRepository<Language> dataRepository,
            ILanguagesService languagesService)
        {
            this.dataRepository = dataRepository;
            this.languagesService = languagesService;
        }

        // GET: Administration/Languages
        public async Task<IActionResult> Index()
        {
            var languageViewModels = this.languagesService.AllLanguages();

            return this.View(await languageViewModels.ToListAsync());
        }

        // GET: Administration/Languages/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Languages/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LanguageInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.languagesService.CreateLanguageAsync(input);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(input);
        }

        // GET: Administration/Languages/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var language = await this.dataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (language == null)
            {
                return this.NotFound();
            }

            return this.View(language);
        }

        // POST: Administration/Languages/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Language language)
        {
            if (id != language.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(language);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.LanguageExists(language.Id))
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

            return this.View(language);
        }

        // GET: Administration/Languages/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var languageViewModel = await this.languagesService.GetLanguageViewModelByIdAsync((int)id);
            if (languageViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(languageViewModel);
        }

        // POST: Administration/Languages/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.languagesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool LanguageExists(int id)
        {
            return this.dataRepository.All().Any(x => x.Id == id);
        }
    }
}

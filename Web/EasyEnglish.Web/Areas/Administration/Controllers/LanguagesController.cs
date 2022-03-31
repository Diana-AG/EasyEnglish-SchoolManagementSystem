namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class LanguagesController : AdministrationController
    {
        private readonly IDeletableEntityRepository<Language> dataRepository;

        public LanguagesController(IDeletableEntityRepository<Language> dataRepository)
        {
            this.dataRepository = dataRepository;
        }

        // GET: Administration/Languages
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dataRepository.AllWithDeleted().ToListAsync());
        }

        // GET: Administration/Languages/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var language = await this.dataRepository.All()
                .FirstOrDefaultAsync(m => m.Id == id);
            if (language == null)
            {
                return this.NotFound();
            }

            return this.View(language);
        }

        // GET: Administration/Languages/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Languages/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Language language)
        {
            if (this.ModelState.IsValid)
            {
                await this.dataRepository.AddAsync(language);
                await this.dataRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(language);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            var language = await this.dataRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);
            if (language == null)
            {
                return this.NotFound();
            }

            return this.View(language);
        }

        // POST: Administration/Languages/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var language = await this.dataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.dataRepository.Delete(language);
            await this.dataRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool LanguageExists(int id)
        {
            return this.dataRepository.All().Any(x => x.Id == id);
        }
    }
}

namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Administration.Levels;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class LevelsController : AdministratorController
    {
        private readonly IDeletableEntityRepository<Level> dataRepository;
        private readonly ILevelsService levelService;

        public LevelsController(
            IDeletableEntityRepository<Level> dataRepository,
            ILevelsService levelService)
        {
            this.dataRepository = dataRepository;
            this.levelService = levelService;
        }

        // GET: Administration/Levels
        public async Task<IActionResult> Index()
        {
            var levelViewModels = this.levelService.AllLevels();

            return this.View(await levelViewModels.ToListAsync());
        }

        // GET: Administration/Levels/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Levels/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LevelInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.levelService.CreateLevelAsync(input);
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(input);
        }

        // GET: Administration/Levels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var level = await this.dataRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (level == null)
            {
                return this.NotFound();
            }

            return this.View(level);
        }

        // POST: Administration/Levels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Level level)
        {
            if (id != level.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dataRepository.Update(level);
                    await this.dataRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.LevelExists(level.Id))
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

            return this.View(level);
        }

        // GET: Administration/Levels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var levelViewModel = await this.levelService.GetLevelViewModelByIdAsync((int)id);
            if (levelViewModel == null)
            {
                return this.NotFound();
            }

            return this.View(levelViewModel);
        }

        // POST: Administration/Levels/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.levelService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool LevelExists(int id)
        {
            return this.dataRepository.All().Any(x => x.Id == id);
        }
    }
}

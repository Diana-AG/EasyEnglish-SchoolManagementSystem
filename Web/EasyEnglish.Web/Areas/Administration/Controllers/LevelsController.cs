namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class LevelsController : AdministratorController
    {
        private readonly ApplicationDbContext dbContext;

        public LevelsController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        // GET: Administration/Levels
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dbContext.Levels.ToListAsync());
        }

        // GET: Administration/Levels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var level = await this.dbContext.Levels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (level == null)
            {
                return this.NotFound();
            }

            return this.View(level);
        }

        // GET: Administration/Levels/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Levels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Level level)
        {
            if (this.ModelState.IsValid)
            {
                this.dbContext.Add(level);
                await this.dbContext.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(level);
        }

        // GET: Administration/Levels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var level = await this.dbContext.Levels.FindAsync(id);
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
                    this.dbContext.Update(level);
                    await this.dbContext.SaveChangesAsync();
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

            var level = await this.dbContext.Levels
                .FirstOrDefaultAsync(m => m.Id == id);
            if (level == null)
            {
                return this.NotFound();
            }

            return this.View(level);
        }

        // POST: Administration/Levels/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var level = await this.dbContext.Levels.FindAsync(id);
            this.dbContext.Levels.Remove(level);
            await this.dbContext.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool LevelExists(int id)
        {
            return this.dbContext.Levels.Any(e => e.Id == id);
        }
    }
}

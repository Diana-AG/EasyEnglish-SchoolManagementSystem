namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CourseTypesController : AdministrationController
    {
        private readonly ApplicationDbContext dbContext;

        public CourseTypesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        // GET: Administration/CourseTypes
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this.dbContext.CourseTypes.Include(c => c.Language).Include(c => c.Level);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/CourseTypes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var courseType = await this.dbContext.CourseTypes
                .Include(c => c.Language)
                .Include(c => c.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (courseType == null)
            {
                return this.NotFound();
            }

            return this.View(courseType);
        }

        // GET: Administration/CourseTypes/Create
        public IActionResult Create()
        {
            this.ViewData["LanguageId"] = new SelectList(this.dbContext.Languages, "Id", "Name");
            this.ViewData["LevelId"] = new SelectList(this.dbContext.Levels, "Id", "Name");
            return this.View();
        }

        // POST: Administration/CourseTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LanguageId,LevelId,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] CourseType courseType)
        {
            if (this.dbContext.CourseTypes.Any(x => x.LevelId == courseType.LevelId && x.LanguageId == courseType.LanguageId))
            {
                return this.View(courseType);
            }

            if (this.ModelState.IsValid)
            {
                this.dbContext.Add(courseType);
                await this.dbContext.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["LanguageId"] = new SelectList(this.dbContext.Languages, "Id", "Name", courseType.LanguageId);
            this.ViewData["LevelId"] = new SelectList(this.dbContext.Levels, "Id", "Name", courseType.LevelId);
            return this.View(courseType);
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

        // POST: Administration/CourseTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
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

            var courseType = await this.dbContext.CourseTypes
                .Include(c => c.Language)
                .Include(c => c.Level)
                .FirstOrDefaultAsync(m => m.Id == id);
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
            var courseType = await this.dbContext.CourseTypes.FindAsync(id);
            this.dbContext.CourseTypes.Remove(courseType);
            await this.dbContext.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CourseTypeExists(int id)
        {
            return this.dbContext.CourseTypes.Any(e => e.Id == id);
        }
    }
}

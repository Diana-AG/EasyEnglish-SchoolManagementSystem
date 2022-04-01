namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class ResourcesController : AdministratorController
    {
        private readonly ApplicationDbContext dbContext;

        public ResourcesController(ApplicationDbContext context)
        {
            this.dbContext = context;
        }

        // GET: Administration/Resources
        public async Task<IActionResult> Index()
        {
            return this.View(await this.dbContext.Resources.ToListAsync());
        }

        // GET: Administration/Resources/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resource = await this.dbContext.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return this.NotFound();
            }

            return this.View(resource);
        }

        // GET: Administration/Resources/Create
        public IActionResult Create()
        {
            return this.View();
        }

        // POST: Administration/Resources/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,Url,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Resource resource)
        {
            if (this.ModelState.IsValid)
            {
                this.dbContext.Add(resource);
                await this.dbContext.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            return this.View(resource);
        }

        // GET: Administration/Resources/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resource = await this.dbContext.Resources.FindAsync(id);
            if (resource == null)
            {
                return this.NotFound();
            }

            return this.View(resource);
        }

        // POST: Administration/Resources/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,Url,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Resource resource)
        {
            if (id != resource.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.dbContext.Update(resource);
                    await this.dbContext.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.ResourceExists(resource.Id))
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

            return this.View(resource);
        }

        // GET: Administration/Resources/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resource = await this.dbContext.Resources
                .FirstOrDefaultAsync(m => m.Id == id);
            if (resource == null)
            {
                return this.NotFound();
            }

            return this.View(resource);
        }

        // POST: Administration/Resources/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var resource = await this.dbContext.Resources.FindAsync(id);
            this.dbContext.Resources.Remove(resource);
            await this.dbContext.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool ResourceExists(int id)
        {
            return this.dbContext.Resources.Any(e => e.Id == id);
        }
    }
}

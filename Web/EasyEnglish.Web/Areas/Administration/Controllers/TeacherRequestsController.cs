namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.Controllers;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class TeacherRequestsController : BaseController
    {
        private readonly ApplicationDbContext _context;

        public TeacherRequestsController(ApplicationDbContext context)
        {
            this._context = context;
        }

        // GET: Administration/TeacherRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = this._context.TeacherRequests.Include(t => t.User);
            return this.View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/TeacherRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var teacherRequest = await this._context.TeacherRequests
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherRequest == null)
            {
                return this.NotFound();
            }

            return this.View(teacherRequest);
        }

        // GET: Administration/TeacherRequests/Create
        public IActionResult Create()
        {
            this.ViewData["UserId"] = new SelectList(this._context.Users, "Id", "Id");
            return this.View();
        }

        // POST: Administration/TeacherRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] TeacherRequest teacherRequest)
        {
            if (this.ModelState.IsValid)
            {
                this._context.Add(teacherRequest);
                await this._context.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["UserId"] = new SelectList(this._context.Users, "Id", "Id", teacherRequest.UserId);
            return this.View(teacherRequest);
        }

        // GET: Administration/TeacherRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var teacherRequest = await this._context.TeacherRequests.FindAsync(id);
            if (teacherRequest == null)
            {
                return this.NotFound();
            }

            this.ViewData["UserId"] = new SelectList(this._context.Users, "Id", "Id", teacherRequest.UserId);
            return this.View(teacherRequest);
        }

        // POST: Administration/TeacherRequests/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] TeacherRequest teacherRequest)
        {
            if (id != teacherRequest.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this._context.Update(teacherRequest);
                    await this._context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.TeacherRequestExists(teacherRequest.Id))
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

            this.ViewData["UserId"] = new SelectList(this._context.Users, "Id", "Id", teacherRequest.UserId);
            return this.View(teacherRequest);
        }

        // GET: Administration/TeacherRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var teacherRequest = await this._context.TeacherRequests
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherRequest == null)
            {
                return this.NotFound();
            }

            return this.View(teacherRequest);
        }

        // POST: Administration/TeacherRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherRequest = await this._context.TeacherRequests.FindAsync(id);
            this._context.TeacherRequests.Remove(teacherRequest);
            await this._context.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool TeacherRequestExists(int id)
        {
            return this._context.TeacherRequests.Any(e => e.Id == id);
        }
    }
}

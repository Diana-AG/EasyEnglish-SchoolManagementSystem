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
    public class TeacherRequestsController : AdministratorController
    {
        private readonly ApplicationDbContext _context;

        public TeacherRequestsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Administration/TeacherRequests
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.TeacherRequests.Include(t => t.User);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Administration/TeacherRequests/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherRequest = await _context.TeacherRequests
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherRequest == null)
            {
                return NotFound();
            }

            return View(teacherRequest);
        }

        // GET: Administration/TeacherRequests/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Administration/TeacherRequests/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] TeacherRequest teacherRequest)
        {
            if (ModelState.IsValid)
            {
                _context.Add(teacherRequest);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", teacherRequest.UserId);
            return View(teacherRequest);
        }

        // GET: Administration/TeacherRequests/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherRequest = await _context.TeacherRequests.FindAsync(id);
            if (teacherRequest == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", teacherRequest.UserId);
            return View(teacherRequest);
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
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(teacherRequest);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TeacherRequestExists(teacherRequest.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", teacherRequest.UserId);
            return View(teacherRequest);
        }

        // GET: Administration/TeacherRequests/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var teacherRequest = await _context.TeacherRequests
                .Include(t => t.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (teacherRequest == null)
            {
                return NotFound();
            }

            return View(teacherRequest);
        }

        // POST: Administration/TeacherRequests/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var teacherRequest = await _context.TeacherRequests.FindAsync(id);
            _context.TeacherRequests.Remove(teacherRequest);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TeacherRequestExists(int id)
        {
            return _context.TeacherRequests.Any(e => e.Id == id);
        }
    }
}

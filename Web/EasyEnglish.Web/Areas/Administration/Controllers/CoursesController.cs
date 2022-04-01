namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CoursesController : AdministratorController
    {
        private readonly IDeletableEntityRepository<Course> coursesRepository;
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;
        private readonly IDeletableEntityRepository<ApplicationUser> usersRepository;

        public CoursesController(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<CourseType> courseTypesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository)
        {
            this.coursesRepository = coursesRepository;
            this.courseTypesRepository = courseTypesRepository;
            this.usersRepository = usersRepository;
        }

        // GET: Administration/Courses
        public async Task<IActionResult> Index()
        {
            var courses = this.coursesRepository.All()
                .Include(c => c.CourseType)
                .Include(c => c.Teacher)
                .Select(c => new IndexCourseViewModel
                {
                    Id = c.Id,
                    StartDate = c.StartDate,
                    EndDate = c.EndDate,
                    Price = c.Price,
                    Description = c.Description,
                    CourseType = new CourseTypeViewModel
                    {
                        Id = c.CourseTypeId,
                        Name = $"{c.CourseType.Language.Name} - {c.CourseType.Level.Name}",
                    },
                    Teacher = new TeacherViewModel
                    {
                        Id = c.TeacherId,
                        Name = c.Teacher.FullName,
                    },
                });

            return this.View(await courses.ToListAsync());
        }

        // GET: Administration/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.coursesRepository.All()
                .Include(c => c.CourseType)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // GET: Administration/Courses/Create
        public IActionResult Create()
        {
            this.ViewData["TeacherId"] = new SelectList(this.usersRepository.All(), "Id", "FullName");
            this.ViewData["CourseTypeId"] = new SelectList(this.courseTypesRepository.All(), "Id", "Description");
            return this.View();
        }

        // POST: Administration/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCourseInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                var course = new Course
                {
                    StartDate = input.StartDate,
                    EndDate = input.EndDate,
                    Price = input.Price,
                    TeacherId = input.TeacherId,
                    CourseTypeId = input.CourseTypeId,
                    Description = input.Description,
                };

                await this.coursesRepository.AddAsync(course);
                await this.coursesRepository.SaveChangesAsync();
                return this.RedirectToAction(nameof(this.Index));
            }

            this.ViewData["CourseTypeId"] = new SelectList(this.courseTypesRepository.All(), "Id", "Description", input.CourseTypeId);
            this.ViewData["TeacherId"] = new SelectList(this.usersRepository.All(), "Id", "FullName", input.TeacherId);
            return this.View(input);
        }

        // GET: Administration/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.coursesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            if (course == null)
            {
                return this.NotFound();
            }

            this.ViewData["CourseTypeId"] = new SelectList(this.courseTypesRepository.All(), "Id", "Description", course.CourseTypeId);
            this.ViewData["TeacherId"] = new SelectList(this.usersRepository.All(), "Id", "FullName", course.TeacherId);
            return this.View(course);
        }

        // POST: Administration/Courses/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("StartDate,EndDate,Price,TeacherId,CourseTypeId,Description,IsDeleted,DeletedOn,Id,CreatedOn,ModifiedOn")] Course course)
        {
            if (id != course.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    this.coursesRepository.Update(course);
                    await this.coursesRepository.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!this.CourseExists(course.Id))
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

            this.ViewData["CourseTypeId"] = new SelectList(this.courseTypesRepository.All(), "Id", "Description", course.CourseTypeId);
            this.ViewData["TeacherId"] = new SelectList(this.usersRepository.All(), "Id", "FullName", course.TeacherId);
            return this.View(course);
        }

        // GET: Administration/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.coursesRepository.All()
                .Include(c => c.CourseType)
                .Include(c => c.Teacher)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // POST: Administration/Courses/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var course = await this.coursesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            this.coursesRepository.Delete(course);
            await this.coursesRepository.SaveChangesAsync();
            return this.RedirectToAction(nameof(this.Index));
        }

        private bool CourseExists(int id)
        {
            return this.coursesRepository.All().Any(x => x.Id == id);
        }
    }
}

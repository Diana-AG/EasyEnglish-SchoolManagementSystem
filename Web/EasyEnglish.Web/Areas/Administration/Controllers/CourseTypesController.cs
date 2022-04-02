﻿namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data;
    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Web.Areas.Administration.ViewModels;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CourseTypesController : AdministratorController
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IDeletableEntityRepository<CourseType> courseTypesRepository;

        public CourseTypesController(
            ApplicationDbContext context,
            IDeletableEntityRepository<CourseType> courseTypesRepository)
        {
            this.dbContext = context;
            this.courseTypesRepository = courseTypesRepository;
        }

        // GET: Administration/CourseTypes
        public async Task<IActionResult> Index()
        {
            var courseTypes = this.courseTypesRepository.All()
                .Include(x => x.Language)
                .Include(x => x.Level)
                .Select(x => new CourseTypeViewModel
                {
                    Id = x.Id,
                    Name = $"{x.Language.Name} - {x.Level.Name}",
                });

            return this.View(await courseTypes.ToListAsync());
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

        // GET: Administration/CourseTypes/AddResource/5
        public async Task<IActionResult> AddResource(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var resources = await this.dbContext.Resources.ToListAsync();

            var courseTypeResouce = new CourseTypeResourseViewModel
            {
                CourseId = (int)id,
                Resources = resources,
            };

            return this.View(courseTypeResouce);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddResource(CourseTypereSourseInputModel model)
        {
            if (model.Id == null || model.CourseId == null)
            {
                return this.NotFound();
            }

            var courseType = await this.dbContext.CourseTypes.FindAsync(model.CourseId);
            var resource = await this.dbContext.Resources.FindAsync(model.Id);
            if (courseType == null || resource == null)
            {
                return this.NotFound();
            }

            courseType.Resources.Add(resource);
            await this.dbContext.SaveChangesAsync();

            return this.RedirectToAction(nameof(this.Index));
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

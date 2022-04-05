namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
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
        private readonly ICourseService courseService;
        private readonly ITeachersService teachersService;
        private readonly ICourseTypeService courseTypeService;

        public CoursesController(
            IDeletableEntityRepository<Course> coursesRepository,
            IDeletableEntityRepository<CourseType> courseTypesRepository,
            IDeletableEntityRepository<ApplicationUser> usersRepository,
            ICourseService courseService,
            ITeachersService teachersService,
            ICourseTypeService courseTypeService)
        {
            this.coursesRepository = coursesRepository;
            this.courseTypesRepository = courseTypesRepository;
            this.usersRepository = usersRepository;
            this.courseService = courseService;
            this.teachersService = teachersService;
            this.courseTypeService = courseTypeService;
        }

        // GET: Administration/Courses
        public async Task<IActionResult> Index()
        {
            var courses = this.courseService.AllCourses();

            return this.View(await courses.ToListAsync());
        }

        // GET: Administration/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.courseService.GetCourseViewModelByIdAsync(id);

            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // GET: Administration/Courses/Create
        public IActionResult Create()
        {
            var viewModel = new CourseInputModel();

            viewModel.TeachersItems = this.teachersService.GetAllAsKeyValuePair();
            viewModel.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }

        // POST: Administration/Courses/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseInputModel input)
        {
            if (this.ModelState.IsValid)
            {
                await this.courseService.CreateCourseAsync(input);

                return this.RedirectToAction(nameof(this.Index));
            }

            input.TeachersItems = this.teachersService.GetAllAsKeyValuePair();
            input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();

            return this.View(input);
        }

        // GET: Administration/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = await this.courseService.GetCourseByIdAsync(id);
            if (course == null)
            {
                return this.NotFound();
            }

            var viewModel = await this.courseService.GetCourseViewModelByIdAsync(id);
            viewModel.TeachersItems = this.teachersService.GetAllAsKeyValuePair();
            viewModel.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();

            //this.ViewData["CourseTypeItems"] = new SelectList(this.courseTypesRepository.All(), "Id", "Description", course.CourseTypeId);
            //this.ViewData["TeacherItems"] = new SelectList(this.usersRepository.All(), "Id", "FullName", course.TeacherId);

            return this.View(viewModel);
        }

        // POST: Administration/Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, CourseInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                try
                {
                    await this.courseService.EditCourseAsync(input);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (await this.courseService.GetCourseByIdAsync(id) == null)
                    {
                        return this.NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                return this.RedirectToAction(nameof(this.Details), new { Id = id });
            }

            input.TeachersItems = this.teachersService.GetAllAsKeyValuePair();
            input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();

            return this.View(input);
        }

        // GET: Administration/Courses/AddStudent/5
        public async Task<IActionResult> AddStudent(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var students = this.courseService.AllStudents((int)id);

            return this.View(await students.ToListAsync());
        }

        // POST: Administration/Courses/AddStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddStudent(CourseStudentInputModel input)
        {
            if (input.CourseId == null || input.StudentId == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.courseService.AddStudentAsync(input);
                return this.RedirectToAction(nameof(this.Details), new { id = input.CourseId });
            }

            return this.View();
        }

        // POST: Administration/Courses/RemoveStudent
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RemoveStudent(CourseStudentInputModel input)
        {
            if (input.CourseId == null || input.StudentId == null)
            {
                return this.NotFound();
            }

            if (this.ModelState.IsValid)
            {
                await this.courseService.RemoveStudentAsync(input);
                return this.RedirectToAction(nameof(this.Details), new { id = input.CourseId });
            }

            return this.View();
        }

        // GET: Administration/Courses/AddStudents/5
        public async Task<IActionResult> AddStudents(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            //var course = await this.coursesRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            //if (course == null)
            //{
            //    return this.NotFound();
            //}

            var viewModel = new CourseAddStudentViewModel { CourseId = (int)id };
            var allStudents = this.usersRepository.All()
                .Select(x => new StudentViewModel
                {
                    Id = x.Id,
                    Name = x.FullName,
                    Email = x.Email,
                }).ToList();

            //viewModel.Students = this.usersRepository.All()
            //    .Select(x => new StudentViewModel
            //    {
            //        Id = x.Id,
            //        Name = x.FullName,
            //        Email = x.Email,
            //    }).ToList();

            return this.View(viewModel);
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

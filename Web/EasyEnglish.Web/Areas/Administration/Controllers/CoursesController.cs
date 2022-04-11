namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;

    [Area("Administration")]
    public class CoursesController : AdministratorController
    {
        private readonly ICourseService courseService;
        private readonly ITeachersService teachersService;
        private readonly ICourseTypeService courseTypeService;
        private readonly ITrainingFormsService trainingFormsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            ICourseService courseService,
            ITeachersService teachersService,
            ICourseTypeService courseTypeService,
            ITrainingFormsService trainingFormsService,
            UserManager<ApplicationUser> userManager)
        {
            this.courseService = courseService;
            this.teachersService = teachersService;
            this.courseTypeService = courseTypeService;
            this.trainingFormsService = trainingFormsService;
            this.userManager = userManager;
        }

        // GET: Administration/Courses
        public IActionResult Index(int id = 1)
        {
            if (id < 1)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 8;
            var viewModel = new CourseListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                CoursesCount = this.courseService.GetCount(),
                Courses = this.courseService.GetAll<CourseInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        // GET: Administration/Courses/Create
        public IActionResult Create()
        {
            var viewModel = new CourseInputModel();
            viewModel.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
            viewModel.TrainingFormsItems = this.trainingFormsService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }

        // POST: Administration/Courses/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
                input.TrainingFormsItems = this.trainingFormsService.GetAllAsKeyValuePair();

                return this.View(input);
            }

            var user = await this.userManager.GetUserAsync(this.User);

            try
            {
                await this.courseService.CreateAsync(input, user.Id);
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
                input.TrainingFormsItems = this.trainingFormsService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            this.TempData["Message"] = "Course added successfully.";

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/Courses/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = this.courseService.GetById<CourseViewModel>((int)id);

            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // GET: Administration/Courses/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = this.courseService.GetById<EditCourseInputModel>((int)id);
            if (course == null)
            {
                return this.NotFound();
            }

            var viewModel = this.courseService.GetById<EditCourseInputModel>((int)id);
            viewModel.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
            viewModel.TrainingFormsItems = this.trainingFormsService.GetAllAsKeyValuePair();
            return this.View(viewModel);
        }

        // POST: Administration/Courses/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCourseInputModel input)
        {
            if (id != input.Id)
            {
                return this.NotFound();
            }

            if (!this.ModelState.IsValid)
            {
                input.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
                input.TrainingFormsItems = this.trainingFormsService.GetAllAsKeyValuePair();

                return this.View(input);
            }

            await this.courseService.UpdateAsync(id, input);
            return this.RedirectToAction(nameof(this.Details), new { Id = id });
        }

        // GET: Administration/Courses/AddStudent/5
        public async Task<IActionResult> AddStudent(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var viewModel = this.courseService.AllStudents((int)id);

            return this.View(viewModel);
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

            var course = this.courseService.GetById<CourseViewModel>((int)input.CourseId);
            var student = this.courseService.GetById<StudentViewModel>(input.StudentId);
            if (course == null || student == null)
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

            var course = this.courseService.GetById<CourseViewModel>((int)input.CourseId);
            var student = this.courseService.GetById<StudentViewModel>(input.StudentId);
            if (course == null || student == null)
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

        // GET: Administration/Courses/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return this.NotFound();
            }

            var course = this.courseService.GetById<CourseViewModel>((int)id);
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
            await this.courseService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

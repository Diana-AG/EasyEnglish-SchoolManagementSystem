namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class CoursesController : AdministratorController
    {
        private readonly ICoursesService courseService;
        private readonly ITeachersService teachersService;
        private readonly ICourseTypesService courseTypeService;
        private readonly ITrainingFormsService trainingFormsService;
        private readonly UserManager<ApplicationUser> userManager;

        public CoursesController(
            ICoursesService courseService,
            ITeachersService teachersService,
            ICourseTypesService courseTypeService,
            ITrainingFormsService trainingFormsService,
            UserManager<ApplicationUser> userManager)
        {
            this.userManager = userManager;
            this.courseService = courseService;
            this.teachersService = teachersService;
            this.courseTypeService = courseTypeService;
            this.trainingFormsService = trainingFormsService;
        }

        // GET: Administration/Courses
        public async Task<IActionResult> Index(int id = 1)
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
                Courses = await this.courseService.GetAll<CourseInListViewModel>(id, ItemsPerPage),
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

            this.ViewData[MessageConstant.SuccessMessage] = "Course added successfully.";

            return this.RedirectToAction(nameof(this.Index));
        }

        // GET: Administration/Courses/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var course = await this.courseService.GetByIdAsync<CourseViewModel>(id);

            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

        // GET: Administration/Courses/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var viewModel = await this.courseService.GetByIdAsync<EditCourseInputModel>(id);
            if (viewModel == null)
            {
                return this.NotFound();
            }

            viewModel.CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair();
            viewModel.TrainingFormsItems = this.trainingFormsService.GetAllAsKeyValuePair();
            return this.View(viewModel);
        }

        // POST: Administration/Courses/Edit/5
        [HttpPost]
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
        public async Task<IActionResult> AddStudent(int id)
        {
            var viewModel = await this.courseService.GetAvailableStudents(id);

            return this.View(viewModel);
        }

        // POST: Administration/Courses/AddStudent
        [HttpPost]
        public async Task<IActionResult> AddStudent(CourseStudentInputModel input)
        {
            var course = await this.courseService.GetByIdAsync<CourseViewModel>(input.CourseId);
            var student = await this.courseService.GetByIdAsync<StudentViewModel>(input.StudentId);
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
        public async Task<IActionResult> RemoveStudent(CourseStudentInputModel input)
        {
            var course = await this.courseService.GetByIdAsync<CourseViewModel>(input.CourseId);
            var student = await this.courseService.GetByIdAsync<StudentViewModel>(input.StudentId);
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
        public async Task<IActionResult> Delete(int id)
        {
            var course = await this.courseService.GetByIdAsync<CourseViewModel>(id);
            if (course == null)
            {
                return this.NotFound();
            }

            this.ViewData[MessageConstant.WarningMessage] = "Please, be careful! Deletion may result in data loss";

            return this.View(course);
        }

        // POST: Administration/Courses/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.courseService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

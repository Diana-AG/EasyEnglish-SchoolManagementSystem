namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EasyEnglish.Common;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Constants;
    using EasyEnglish.Web.Controllers;
    using EasyEnglish.Web.ViewModels.Administration.Courses;
    using EasyEnglish.Web.ViewModels.Administration.Students;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.ManagerRoleName}, {GlobalConstants.TeacherRoleName}")]
    public class CoursesController : BaseController
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
                ItemsCount = this.courseService.GetCount(),
                Courses = await this.courseService.GetAll<CourseInListViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CourseInputModel
            {
                CourseTypeItems = this.courseTypeService.GetAllAsKeyValuePair(),
                TrainingFormsItems = this.trainingFormsService.GetAllAsKeyValuePair(),
            };

            return this.View(viewModel);
        }

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

        public async Task<IActionResult> Details(int id)
        {
            var course = await this.courseService.GetByIdAsync<CourseViewModel>(id);

            if (course == null)
            {
                return this.NotFound();
            }

            return this.View(course);
        }

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

        public async Task<IActionResult> AddStudent(int id)
        {
            var viewModel = await this.courseService.GetAvailableStudents(id);

            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddStudent(CourseStudentInputModel input)
        {
            var course = await this.courseService.GetByIdAsync<CourseViewModel>(input.CourseId);
            var student = await this.courseService.GetByIdAsync<StudentsViewModel>(input.StudentId);
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

        [HttpPost]
        public async Task<IActionResult> RemoveStudent(CourseStudentInputModel input)
        {
            var course = await this.courseService.GetByIdAsync<CourseViewModel>(input.CourseId);
            var student = await this.courseService.GetByIdAsync<StudentsViewModel>(input.StudentId);
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

        [HttpPost]
        [ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await this.courseService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

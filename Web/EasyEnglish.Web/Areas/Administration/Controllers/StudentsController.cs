namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System;
    using System.Threading.Tasks;

    using EasyEnglish.Common;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Controllers;
    using EasyEnglish.Web.ViewModels.Administration.Students;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.ManagerRoleName}, {GlobalConstants.TeacherRoleName}")]
    public class StudentsController : BaseController
    {
        private readonly IStudentsService studentsService;
        private readonly IWebHostEnvironment environment;

        public StudentsController(
            IStudentsService studentsService,
            IWebHostEnvironment environment)
        {
            this.studentsService = studentsService;
            this.environment = environment;
        }

        public async Task<IActionResult> Index(int id = 1)
        {
            if (id < 1)
            {
                return this.NotFound();
            }

            const int ItemsPerPage = 8;
            var viewModel = new StudentsListViewModel
            {
                ItemsPerPage = ItemsPerPage,
                PageNumber = id,
                ItemsCount = this.studentsService.GetCount(),
                Students = await this.studentsService.GetAllActive<StudentsViewModel>(id, ItemsPerPage),
            };

            return this.View(viewModel);
        }

        public IActionResult AddPortfolio(string id)
        {
            var viewModel = new StudentPortfolioInputModel { StudentId = id };
            return this.View(viewModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddPortfolio(StudentPortfolioInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(input);
            }

            try
            {
                await this.studentsService.UploadImagesAsync(input, $"{this.environment.WebRootPath}/students");
            }
            catch (Exception ex)
            {
                this.ModelState.AddModelError(string.Empty, ex.Message);
                return this.View(input);
            }

            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

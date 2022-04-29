namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using EasyEnglish.Common;
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.Controllers;
    using EasyEnglish.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    [Authorize(Roles = $"{GlobalConstants.AdministratorRoleName}, {GlobalConstants.ManagerRoleName}, {GlobalConstants.TeacherRoleName} ")]
    public class DashboardController : BaseController
    {
        private readonly IGetCountsService countsService;

        public DashboardController(IGetCountsService countsService)
        {
            this.countsService = countsService;
        }

        public IActionResult Index()
        {
            var countsDto = this.countsService.GetCounts();
            var viewModel = new IndexViewModel
            {
                CoursesCount = countsDto.CoursesCount,
                LanguagesCount = countsDto.LanguagesCount,
                StudentsCount = countsDto.StudentsCount,
                TeachersCount = countsDto.TeachersCount,
            };

            return this.View(viewModel);
        }
    }
}

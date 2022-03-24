namespace EasyEnglish.Web.Controllers
{
    using System.Diagnostics;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels;
    using EasyEnglish.Web.ViewModels.Home;
    using Microsoft.AspNetCore.Mvc;

    public class HomeController : BaseController
    {
        private readonly IGetCountsService countsService;

        public HomeController(IGetCountsService countsService)
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

        public IActionResult Privacy()
        {
            return this.View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return this.View(
                new ErrorViewModel { RequestId = Activity.Current?.Id ?? this.HttpContext.TraceIdentifier });
        }
    }
}

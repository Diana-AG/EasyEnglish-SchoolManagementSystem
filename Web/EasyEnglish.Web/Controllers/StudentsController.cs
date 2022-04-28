namespace EasyEnglish.Web.Controllers
{
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Students;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    public class StudentsController : BaseController
    {
        private readonly ICoursesService courseService;

        public StudentsController(ICoursesService courseService)
        {
            this.courseService = courseService;
        }

        public async Task<IActionResult> MyCourses()
        {
            var viewModel = new StudentCoursesListViewModel
            {
                Courses = await this.courseService.GetAll<StudentCoursesInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult MyPortfolio()
        {
            return this.View();
        }
    }
}

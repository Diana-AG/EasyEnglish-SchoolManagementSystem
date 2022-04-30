namespace EasyEnglish.Web.Controllers
{
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Courses;
    using EasyEnglish.Web.ViewModels.CourseTypes;
    using Microsoft.AspNetCore.Mvc;

    public class CoursesController : Controller
    {
        private readonly ILevelsService levelsService;
        private readonly ITeachersService teachersService;
        private readonly ILanguagesService languagesService;
        private readonly ICurrenciesService currenciesService;
        private readonly ICourseTypesService courseTypesService;

        public CoursesController(
             ILevelsService levelsService,
             ITeachersService teachersService,
             ILanguagesService languagesService,
             ICurrenciesService currenciesService,
             ICourseTypesService courseTypesService)
        {
            this.levelsService = levelsService;
            this.teachersService = teachersService;
            this.languagesService = languagesService;
            this.currenciesService = currenciesService;
            this.courseTypesService = courseTypesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CourseTypeListViewModel
            {
                CourseTypes = await this.courseTypesService.GetAllAsync<CourseTypeInListViewModel>(),
            };

            return this.View(viewModel);
        }

        public IActionResult Create()
        {
            var viewModel = new CreateCourseInputModel();

            viewModel.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
            viewModel.TeachersItems = this.teachersService.GetAllAsKeyValuePair();
            viewModel.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
            viewModel.CurrenciesItems = this.currenciesService.GetAllAsKeyValuePair();
            return this.View(viewModel);
        }

        [HttpPost]
        public IActionResult Create(CreateCourseInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                input.LevelsItems = this.levelsService.GetAllAsKeyValuePair();
                input.TeachersItems = this.teachersService.GetAllAsKeyValuePair();
                input.LanguagesItems = this.languagesService.GetAllAsKeyValuePair();
                input.CurrenciesItems = this.currenciesService.GetAllAsKeyValuePair();
                return this.View(input);
            }

            return this.Json(input);
        }
    }
}

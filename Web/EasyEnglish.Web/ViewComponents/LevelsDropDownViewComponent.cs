namespace EasyEnglish.Web.ViewComponents
{
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class LevelsDropDownViewComponent : ViewComponent
    {
        private readonly ILevelsService levelsService;

        public LevelsDropDownViewComponent(ILevelsService levelsService)
        {
            this.levelsService = levelsService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new DropDownViewModel();

            viewModel.Items = this.levelsService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }
    }
}

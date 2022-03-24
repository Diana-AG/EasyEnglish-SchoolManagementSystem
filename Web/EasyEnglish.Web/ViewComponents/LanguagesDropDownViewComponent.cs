namespace EasyEnglish.Web.ViewComponents
{
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class LanguagesDropDownViewComponent : ViewComponent
    {
        private readonly ILanguagesService languagesService;

        public LanguagesDropDownViewComponent(ILanguagesService languagesService)
        {
            this.languagesService = languagesService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new DropDownViewModel();

            viewModel.Items = this.languagesService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }
    }
}

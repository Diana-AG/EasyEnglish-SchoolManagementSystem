namespace EasyEnglish.Web.ViewComponents
{
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class CurrenciesDropDownViewComponent : ViewComponent
    {
        private readonly ICurrenciesService currenciesService;

        public CurrenciesDropDownViewComponent(ICurrenciesService currenciesService)
        {
            this.currenciesService = currenciesService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new DropDownViewModel();

            viewModel.Items = this.currenciesService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }
    }
}

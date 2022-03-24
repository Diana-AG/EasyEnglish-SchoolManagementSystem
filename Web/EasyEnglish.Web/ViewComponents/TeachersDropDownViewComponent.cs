namespace EasyEnglish.Web.ViewComponents
{
    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.ViewComponents;
    using Microsoft.AspNetCore.Mvc;

    public class TeachersDropDownViewComponent : ViewComponent
    {
        private readonly ITeachersService teacherService;

        public TeachersDropDownViewComponent(ITeachersService teacherService)
        {
            this.teacherService = teacherService;
        }

        public IViewComponentResult Invoke()
        {
            var viewModel = new DropDownViewModel();

            viewModel.Items = this.teacherService.GetAllAsKeyValuePair();

            return this.View(viewModel);
        }
    }
}

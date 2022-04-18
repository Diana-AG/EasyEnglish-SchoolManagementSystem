namespace EasyEnglish.Web.Areas.Administration.Controllers
{
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Administration.Messages;
    using Microsoft.AspNetCore.Mvc;

    [Area("Administration")]
    public class MessagesController : AdministratorController
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = await this.messagesService.GetAllAsync<MessageViewModel>();
            return this.View(viewModel);
        }

        public IActionResult Add()
        {
            return this.View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(MessageInputModel input)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View();
            }

            await this.messagesService.AddAsync(input);

            return this.RedirectToAction(nameof(this.Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await this.messagesService.DeleteAsync(id);
            return this.RedirectToAction(nameof(this.Index));
        }
    }
}

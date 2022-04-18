namespace EasyEnglish.Web.Controllers
{
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Messages;
    using Microsoft.AspNetCore.Mvc;

    public class MessagesController : BaseController
    {
        private readonly IMessagesService messagesService;

        public MessagesController(IMessagesService messagesService)
        {
            this.messagesService = messagesService;
        }

        public async Task<IActionResult> Index()
        {
            var message = await this.messagesService.GetAllAsync<MessageViewModel>();
            var viewModel = new MessageListViewModel { Messages = message };
            return this.View(viewModel);
        }
    }
}

namespace EasyEnglish.Services.Data
{
    using EasyEnglish.Web.ViewModels.Administration.Messages;
    using System.Threading.Tasks;

    public interface IMessagesService
    {
        Task AddAsync(MessageInputModel input);
    }
}

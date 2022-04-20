namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EasyEnglish.Web.ViewModels.Administration.Messages;

    public interface IMessagesService
    {
        Task AddAsync(MessageInputModel input, string userId);

        Task<IEnumerable<T>> GetAllAsync<T>();

        Task DeleteAsync(int id);
    }
}

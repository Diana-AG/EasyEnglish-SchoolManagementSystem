namespace EasyEnglish.Services.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;
    using EasyEnglish.Services.Mapping;
    using EasyEnglish.Web.ViewModels.Administration.Messages;
    using Microsoft.EntityFrameworkCore;

    public class MessagesService : IMessagesService
    {
        private readonly IDeletableEntityRepository<Message> messagesRepository;

        public MessagesService(IDeletableEntityRepository<Message> messagesRepository)
        {
            this.messagesRepository = messagesRepository;
        }

        public async Task AddAsync(MessageInputModel input, string userId)
        {
            var message = new Message
            {
                Description = input.Description,
                AddedByUserId = userId,
                StartDate = input.StartDate,
                EndDate = input.EndDate,
            };

            await this.messagesRepository.AddAsync(message);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var resource = await this.messagesRepository.All().FirstOrDefaultAsync(x => x.Id == id);

            this.messagesRepository.Delete(resource);
            await this.messagesRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAllAsync<T>()
        {
            return await this.messagesRepository.All().Include(x => x.AddedByUser).To<T>().ToListAsync();
        }
    }
}

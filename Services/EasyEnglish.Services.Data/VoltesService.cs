namespace EasyEnglish.Services.Data
{
    using System.Linq;
    using System.Threading.Tasks;

    using EasyEnglish.Data.Common.Repositories;
    using EasyEnglish.Data.Models;

    public class VoltesService : IVoltesService
    {
        private readonly IRepository<Vote> votesRepository;

        public VoltesService(IRepository<Vote> votesRepository)
        {
            this.votesRepository = votesRepository;
        }

        public double GetAverageVotes(int messageId)
        {
            return this.votesRepository.All()
                .Where(x => x.MessageId == messageId)
                .Average(x => x.Value);
        }

        public async Task SetVoteAsync(int messageId, string userId, byte value)
        {
            var vote = this.votesRepository.All()
                .FirstOrDefault(x => x.MessageId == messageId && x.UserId == userId);
            if (vote == null)
            {
                vote = new Vote
                {
                    MessageId = messageId,
                    UserId = userId,
                };

                await this.votesRepository.AddAsync(vote);
            }

            vote.Value = value;
            await this.votesRepository.SaveChangesAsync();
        }
    }
}

namespace EasyEnglish.Services.Data
{
    using System.Threading.Tasks;

    public interface IVoltesService
    {
        Task SetVoteAsync(int messageId, string userId, byte value);

        double GetAverageVotes(int messageId);
    }
}

namespace EasyEnglish.Web.Controllers
{
    using System.Security.Claims;
    using System.Threading.Tasks;

    using EasyEnglish.Services.Data;
    using EasyEnglish.Web.ViewModels.Votes;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("api/[controller]")]
    public class VotesController : BaseController
    {
        private readonly IVoltesService votesService;

        public VotesController(IVoltesService votesService)
        {
            this.votesService = votesService;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<PostVoteResponseModel>> Post(PostVoteInputModel input)
        {
            var userId = this.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            await this.votesService.SetVoteAsync(input.MessageId, userId, input.Value);
            var averageVotes = this.votesService.GetAverageVotes(input.MessageId);
            return new PostVoteResponseModel { AverageVote = averageVotes };
        }
    }
}

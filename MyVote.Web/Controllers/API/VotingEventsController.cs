
namespace MyVote.Web.Controllers.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyVote.Common.Models;
    using MyVote.Web.Data.Repositories;
    using MyVote.Web.Helpers;
    using System.Threading.Tasks;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VotingEventsController : Controller
    {
        private readonly IVotingEventRepository votingEventRepository;
        private readonly IUserHelper userHelper;

        public VotingEventsController(IVotingEventRepository votingEventRepository, IUserHelper userHelper)
        {
            this.votingEventRepository = votingEventRepository;
            this.userHelper = userHelper;
        }

        [HttpGet]
        public IActionResult GetVotingEvents()
        {
            return Ok(this.votingEventRepository.GetAllWithCandidates());
        }

        [HttpPost]
        [Route("AddVote")]
        public async Task<IActionResult> AddVote([FromBody] VoteRequest request)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "Bad request"
                });
            }

            var user = await this.userHelper.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                return this.BadRequest(new Response
                {
                    IsSuccess = false,
                    Message = "This email is not assigned to any user."
                });
            }

            var vote = new Data.Entities.Vote
            {
                User = user
            };

            var result = await this.votingEventRepository.AddVoteAsync(vote, request.CandidateId);
            if (result)
            {
                return Ok(new Response
                {
                    IsSuccess = true,
                    Message = "Your vote has been correctly registered!"
                });

            }
            else
            {
                return Ok(new Response
                {
                    IsSuccess = false,
                    Message = $"candidate not found { request.CandidateId}"
                });
            }

        }
    }
}


namespace MyVote.Web.Controllers.API
{
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;
    using MyVote.Web.Data.Repositories;

    [Route("api/[Controller]")]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class VotingEventsController : Controller
    {
        private readonly IVotingEventRepository votingEventRepository;

        public VotingEventsController(IVotingEventRepository votingEventRepository)
        {
            this.votingEventRepository = votingEventRepository;
        }

        [HttpGet]
        public IActionResult GetVotingEvents()
        {
            return Ok(this.votingEventRepository.GetAllWithCandidates());
        }
    }
}

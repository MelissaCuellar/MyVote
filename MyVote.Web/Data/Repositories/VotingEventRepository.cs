
namespace MyVote.Web.Data.Repositories
{
    using Entities;
    public class VotingEventRepository:GenericRepository<VotingEvent>, IVotingEventRepository
    {
        public VotingEventRepository(DataContext context):base(context)
        {

        }
    }
}

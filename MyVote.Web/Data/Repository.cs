
namespace MyVote.Web.Data
{
    using MyVote.Web.Data.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class Repository : IRepository
    {
        private readonly DataContext context;

        public Repository(DataContext context)
        {
            this.context = context;
        }

        public IEnumerable<VotingEvent> GetVotingEvents()
        {
            return this.context.VotingEvents.OrderBy(p => p.Name);
        }

        public VotingEvent GetVotingEvent(int id)
        {
            return this.context.VotingEvents.Find(id);
        }

        public void AddVotingEvent(VotingEvent votingEvent)
        {
            this.context.VotingEvents.Add(votingEvent);
        }

        public void UpdateVotingEvent(VotingEvent votingEvent)
        {
            this.context.VotingEvents.Update(votingEvent);
        }

        public void RemoveVotingEvent(VotingEvent votingEvent)
        {
            this.context.VotingEvents.Remove(votingEvent);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await this.context.SaveChangesAsync() > 0;
        }

        public bool VotingEventExists(int id)
        {
            return this.context.VotingEvents.Any(p => p.Id == id);
        }
    }

}

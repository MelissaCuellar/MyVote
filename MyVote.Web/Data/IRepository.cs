

namespace MyVote.Web.Data
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Entities;

    public interface IRepository
    {
        void AddVotingEvent(VotingEvent votingEvent);

        VotingEvent GetVotingEvent(int id);

        IEnumerable<VotingEvent> GetVotingEvents();

        void RemoveVotingEvent(VotingEvent votingEvent);

        Task<bool> SaveAllAsync();

        void UpdateVotingEvent(VotingEvent votingEvent);

        bool VotingEventExists(int id);
    }
}
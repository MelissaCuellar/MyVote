
namespace MyVote.Web.Data.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Microsoft.EntityFrameworkCore;
    using MyVote.Web.Models;

    public class VotingEventRepository:GenericRepository<VotingEvent>, IVotingEventRepository
    {
        private readonly DataContext context;

        public VotingEventRepository(DataContext context):base(context)
        {
            this.context = context;
        }

        public async Task AddCandidateAsync(CandidateViewModel model)
        {
            var votingEvent = await this.GetVotingEventWithCandidatesAsync(model.VotingEventId);
            if(votingEvent==null)
            {
                return;
            }

            votingEvent.Candidates.Add(new Candidate
            {
                Name = model.Name,
                Proposal = model.Proposal,
                ImageUrl = model.ImageUrl
            });
            this.context.VotingEvents.Update(votingEvent);
            await this.context.SaveChangesAsync();
        }

        public async Task<int> DeleteCandidateAsync(Candidate candidate)
        {
            var votingEvent = await this.context.VotingEvents
                .Where(c => c.Candidates.Any(ci => ci.Id == candidate.Id))
                .FirstOrDefaultAsync();
            if (votingEvent == null)
            {
                return 0;
            }

            this.context.Candidates.Remove(candidate);
            await this.context.SaveChangesAsync();
            return votingEvent.Id;
        }

        public async Task<Candidate> GetCandidateAsync(int idVote)
        {
            return await this.context.Candidates.FindAsync(idVote);                
        }

        public async Task<VotingEvent> GetVotingEventWithCandidatesAsync(int id)
        {
            return await this.context.VotingEvents
                .Include(c => c.Candidates)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<int> UpdateCandidateAsync(Candidate candidate)
        {
            var votingEvent = await this.context.VotingEvents
                .Where(c => c.Candidates.Any(ci => ci.Id == candidate.Id))
                .FirstOrDefaultAsync();
            if(votingEvent==null)
            {
                return 0;
            }
            this.context.Candidates.Update(candidate);
            await this.context.SaveChangesAsync();
            return votingEvent.Id;
        }
    }
}

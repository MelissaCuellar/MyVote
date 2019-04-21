
namespace MyVote.Web.Data.Repositories
{
    using Entities;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using MyVote.Web.Models;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IVotingEventRepository : IGenericRepository<VotingEvent>
    {
        Task<Candidate> GetCandidateAsync(int idVote);

        Task<VotingEvent> GetVotingEventWithCandidatesAsync(int id);

        Task AddCandidateAsync(CandidateViewModel model, string path);

        Task<int> UpdateCandidateAsync(Candidate candidate);

        Task<int> DeleteCandidateAsync(Candidate candidate);

        IQueryable GetAllWithCandidates();
    }
}

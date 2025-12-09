using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Data.Repositories;

public interface IVoteRepository : IRepository<Vote>
{
    Task<int> GetVoteCountByElectionAsync(int electionId);
    Task<int> GetVoteCountByCandidateAsync(int candidateId, int electionId);
    Task<int> GetVoteCountByPartyAsync(int partyId, int electionId);
    Task<int> GetVoteCountByCountyAsync(int countyId, int electionId);
    Task<IEnumerable<Vote>> GetVotesByElectionAsync(int electionId);
}


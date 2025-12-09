using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Data.Repositories;

public interface ICandidateRepository : IRepository<Candidate>
{
    Task<IEnumerable<Candidate>> GetCandidatesWithPartyAsync();
    Task<Candidate?> GetCandidateWithPartyAsync(int id);
    Task<IEnumerable<Candidate>> GetCandidatesByPartyIdAsync(int partyId);
}


using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Data.Repositories;

public interface IElectionCandidateRepository : IRepository<ElectionCandidate>
{
    Task<IEnumerable<ElectionCandidate>> GetCandidatesByElectionAsync(int electionId);
    Task<bool> IsCandidateInElectionAsync(int electionId, int candidateId);
}


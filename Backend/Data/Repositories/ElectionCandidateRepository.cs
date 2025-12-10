using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Data;

namespace ASP1.Backend.Data.Repositories;

public class ElectionCandidateRepository : Repository<ElectionCandidate>, IElectionCandidateRepository
{
    public ElectionCandidateRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<ElectionCandidate>> GetCandidatesByElectionAsync(int electionId)
    {
        return await _dbSet
            .Where(ec => ec.ElectionId == electionId)
            .Include(ec => ec.Candidate)
            .ThenInclude(c => c.Party)
            .ToListAsync();
    }

    public async Task<bool> IsCandidateInElectionAsync(int electionId, int candidateId)
    {
        return await _dbSet
            .AnyAsync(ec => ec.ElectionId == electionId && ec.CandidateId == candidateId);
    }
}



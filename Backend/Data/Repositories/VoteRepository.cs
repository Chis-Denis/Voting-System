using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Data;

namespace ASP1.Backend.Data.Repositories;

public class VoteRepository : Repository<Vote>, IVoteRepository
{
    public VoteRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<int> GetVoteCountByElectionAsync(int electionId)
    {
        return await _dbSet
            .Where(v => v.ElectionId == electionId)
            .CountAsync();
    }

    public async Task<int> GetVoteCountByCandidateAsync(int candidateId, int electionId)
    {
        return await _dbSet
            .Where(v => v.CandidateId == candidateId && v.ElectionId == electionId)
            .CountAsync();
    }

    public async Task<int> GetVoteCountByPartyAsync(int partyId, int electionId)
    {
        return await _dbSet
            .Where(v => v.ElectionId == electionId && v.Candidate.PartyId == partyId)
            .CountAsync();
    }

    public async Task<int> GetVoteCountByCountyAsync(int countyId, int electionId)
    {
        return await _dbSet
            .Where(v => v.CountyId == countyId && v.ElectionId == electionId)
            .CountAsync();
    }

    public async Task<IEnumerable<Vote>> GetVotesByElectionAsync(int electionId)
    {
        return await _dbSet
            .Where(v => v.ElectionId == electionId)
            .Include(v => v.Candidate)
            .Include(v => v.County)
            .ToListAsync();
    }
}


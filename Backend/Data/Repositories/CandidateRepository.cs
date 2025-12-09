using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Data;

namespace ASP1.Backend.Data.Repositories;

public class CandidateRepository : Repository<Candidate>, ICandidateRepository
{
    public CandidateRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Candidate>> GetCandidatesWithPartyAsync()
    {
        return await _dbSet
            .Include(c => c.Party)
            .ToListAsync();
    }

    public async Task<Candidate?> GetCandidateWithPartyAsync(int id)
    {
        return await _dbSet
            .Include(c => c.Party)
            .FirstOrDefaultAsync(c => c.CandidateId == id);
    }

    public async Task<IEnumerable<Candidate>> GetCandidatesByPartyIdAsync(int partyId)
    {
        return await _dbSet
            .Where(c => c.PartyId == partyId)
            .ToListAsync();
    }
}


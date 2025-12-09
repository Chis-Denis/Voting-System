using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Data;

namespace ASP1.Backend.Data.Repositories;

public class PartyRepository : Repository<Party>, IPartyRepository
{
    public PartyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<Party?> GetPartyWithCandidatesAsync(int id)
    {
        return await _dbSet
            .Include(p => p.Candidates)
            .FirstOrDefaultAsync(p => p.PartyId == id);
    }

    public async Task<Party?> GetPartyByNameAsync(string name)
    {
        return await _dbSet
            .FirstOrDefaultAsync(p => p.Name == name);
    }

    public async Task<IEnumerable<Party>> GetAllPartiesWithCandidatesAsync()
    {
        return await _dbSet
            .Include(p => p.Candidates)
            .ToListAsync();
    }
}


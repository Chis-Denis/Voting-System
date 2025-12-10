using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Data;

namespace ASP1.Backend.Data.Repositories;

public class ElectionRepository : Repository<Election>, IElectionRepository
{
    public ElectionRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Election>> GetOngoingElectionsAsync()
    {
        var now = DateTime.UtcNow;
        return await _dbSet
            .Where(e => e.IsActive && e.StartDate <= now && e.EndDate >= now)
            .OrderBy(e => e.EndDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Election>> GetElectionsByTypeAsync(ElectionType type)
    {
        var now = DateTime.UtcNow;
        return await _dbSet
            .Where(e => e.IsActive && e.Type == type && e.StartDate <= now && e.EndDate >= now)
            .OrderBy(e => e.EndDate)
            .ToListAsync();
    }

    public async Task<IEnumerable<Election>> GetActiveElectionsAsync()
    {
        return await _dbSet
            .Where(e => e.IsActive)
            .OrderByDescending(e => e.StartDate)
            .ToListAsync();
    }
}



using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Data;

namespace ASP1.Backend.Data.Repositories;

public class CountyRepository : Repository<County>, ICountyRepository
{
    public CountyRepository(ApplicationDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<County>> GetAllCountiesAsync()
    {
        return await _dbSet
            .OrderBy(c => c.Name)
            .ToListAsync();
    }

    public async Task<int> GetTotalEligibleVotersAsync()
    {
        return await _dbSet
            .SumAsync(c => c.EligibleVoters);
    }
}


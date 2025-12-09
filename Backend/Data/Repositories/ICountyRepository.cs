using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Data.Repositories;

public interface ICountyRepository : IRepository<County>
{
    Task<IEnumerable<County>> GetAllCountiesAsync();
    Task<int> GetTotalEligibleVotersAsync();
}


using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Data.Repositories;

public interface IPartyRepository : IRepository<Party>
{
    Task<Party?> GetPartyWithCandidatesAsync(int id);
    Task<Party?> GetPartyByNameAsync(string name);
    Task<IEnumerable<Party>> GetAllPartiesWithCandidatesAsync();
}


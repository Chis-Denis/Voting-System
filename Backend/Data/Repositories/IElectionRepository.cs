using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Data.Repositories;

public interface IElectionRepository : IRepository<Election>
{
    Task<IEnumerable<Election>> GetOngoingElectionsAsync();
    Task<IEnumerable<Election>> GetElectionsByTypeAsync(ElectionType type);
    Task<IEnumerable<Election>> GetActiveElectionsAsync();
}


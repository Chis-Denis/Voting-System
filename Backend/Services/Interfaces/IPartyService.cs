using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.Services.Interfaces;

public interface IPartyService
{
    Task<IEnumerable<Party>> GetAllPartiesAsync();
    Task<Party?> GetPartyByIdAsync(int id);
    Task<Party> CreatePartyAsync(Party party);
    Task<Party> UpdatePartyAsync(Party party);
    Task DeletePartyAsync(int id);
    Task<bool> PartyExistsAsync(int id);
    Task<Party?> GetPartyByNameAsync(string name);
}


using ASP1.Backend.Data.Repositories;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Services.Interfaces;

namespace ASP1.Backend.Services.Implementations;

public class PartyService : IPartyService
{
    private readonly IPartyRepository _partyRepository;

    public PartyService(IPartyRepository partyRepository)
    {
        _partyRepository = partyRepository;
    }

    public async Task<IEnumerable<Party>> GetAllPartiesAsync()
    {
        return await _partyRepository.GetAllAsync();
    }

    public async Task<Party?> GetPartyByIdAsync(int id)
    {
        return await _partyRepository.GetPartyWithCandidatesAsync(id);
    }

    public async Task<Party> CreatePartyAsync(Party party)
    {
        return await _partyRepository.AddAsync(party);
    }

    public async Task<Party> UpdatePartyAsync(Party party)
    {
        await _partyRepository.UpdateAsync(party);
        return party;
    }

    public async Task DeletePartyAsync(int id)
    {
        var party = await _partyRepository.GetByIdAsync(id);
        if (party != null)
        {
            await _partyRepository.DeleteAsync(party);
        }
    }

    public async Task<bool> PartyExistsAsync(int id)
    {
        return await _partyRepository.ExistsAsync(id);
    }

    public async Task<Party?> GetPartyByNameAsync(string name)
    {
        return await _partyRepository.GetPartyByNameAsync(name);
    }
}


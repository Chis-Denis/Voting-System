using ASP1.Backend.Data.Repositories;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Implementations;

public class StatisticsService : IStatisticsService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IPartyRepository _partyRepository;

    public StatisticsService(ICandidateRepository candidateRepository, IPartyRepository partyRepository)
    {
        _candidateRepository = candidateRepository;
        _partyRepository = partyRepository;
    }

    public async Task<StatisticsViewModel> GetStatisticsAsync()
    {
        var candidates = await _candidateRepository.GetAllAsync();
        var parties = await _partyRepository.GetAllPartiesWithCandidatesAsync();

        var totalCandidates = candidates.Count();
        var totalParties = parties.Count();

        var candidatesPerParty = parties.Select(p => new PartyStatsViewModel
        {
            PartyName = p.Name,
            CandidateCount = p.Candidates.Count
        }).ToList();

        return new StatisticsViewModel
        {
            TotalCandidates = totalCandidates,
            TotalParties = totalParties,
            CandidatesPerParty = candidatesPerParty
        };
    }
}


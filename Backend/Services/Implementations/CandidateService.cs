using ASP1.Backend.Data.Repositories;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Implementations;

public class CandidateService : ICandidateService
{
    private readonly ICandidateRepository _candidateRepository;
    private readonly IPartyRepository _partyRepository;

    public CandidateService(ICandidateRepository candidateRepository, IPartyRepository partyRepository)
    {
        _candidateRepository = candidateRepository;
        _partyRepository = partyRepository;
    }

    public async Task<IEnumerable<CandidateViewModel>> GetAllCandidatesAsync()
    {
        var candidates = await _candidateRepository.GetCandidatesWithPartyAsync();
        return candidates.Select(MapToViewModel);
    }

    public async Task<CandidateViewModel?> GetCandidateByIdAsync(int id)
    {
        var candidate = await _candidateRepository.GetCandidateWithPartyAsync(id);
        return candidate != null ? MapToViewModel(candidate) : null;
    }

    public async Task<CandidateViewModel> CreateCandidateAsync(CandidateViewModel viewModel)
    {
        var party = await GetOrCreatePartyAsync(viewModel.PartyName);

        var candidate = new Candidate
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            ImageUrl = viewModel.ImageUrl,
            Age = viewModel.Age,
            Position = viewModel.Position,
            PartyId = party?.PartyId
        };

        var createdCandidate = await _candidateRepository.AddAsync(candidate);
        return MapToViewModel(createdCandidate);
    }

    public async Task<CandidateViewModel> UpdateCandidateAsync(int id, CandidateViewModel viewModel)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id);
        if (candidate == null)
            throw new ArgumentException($"Candidate with id {id} not found", nameof(id));

        var party = await GetOrCreatePartyAsync(viewModel.PartyName);

        candidate.Name = viewModel.Name;
        candidate.Description = viewModel.Description;
        candidate.ImageUrl = viewModel.ImageUrl;
        candidate.Age = viewModel.Age;
        candidate.Position = viewModel.Position;
        candidate.PartyId = party?.PartyId;

        await _candidateRepository.UpdateAsync(candidate);
        return MapToViewModel(candidate);
    }

    public async Task DeleteCandidateAsync(int id)
    {
        var candidate = await _candidateRepository.GetByIdAsync(id);
        if (candidate != null)
        {
            await _candidateRepository.DeleteAsync(candidate);
        }
    }

    public async Task<bool> CandidateExistsAsync(int id)
    {
        return await _candidateRepository.ExistsAsync(id);
    }

    private async Task<Party?> GetOrCreatePartyAsync(string? partyName)
    {
        if (string.IsNullOrWhiteSpace(partyName))
            return null;

        var party = await _partyRepository.GetPartyByNameAsync(partyName);
        if (party == null)
        {
            party = new Party
            {
                Name = partyName,
                Description = "Added via candidate form",
                LogoUrl = string.Empty
            };
            await _partyRepository.AddAsync(party);
        }
        return party;
    }

    private CandidateViewModel MapToViewModel(Candidate candidate)
    {
        return new CandidateViewModel
        {
            CandidateId = candidate.CandidateId,
            Name = candidate.Name,
            Description = candidate.Description,
            ImageUrl = candidate.ImageUrl,
            Age = candidate.Age,
            Position = candidate.Position,
            PartyName = candidate.Party?.Name ?? string.Empty
        };
    }
}


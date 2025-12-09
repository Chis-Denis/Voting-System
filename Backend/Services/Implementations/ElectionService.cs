using ASP1.Backend.Data.Repositories;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Implementations;

public class ElectionService : IElectionService
{
    private readonly IElectionRepository _electionRepository;

    public ElectionService(IElectionRepository electionRepository)
    {
        _electionRepository = electionRepository;
    }

    public async Task<IEnumerable<ElectionViewModel>> GetOngoingElectionsAsync()
    {
        var elections = await _electionRepository.GetOngoingElectionsAsync();
        return elections.Select(MapToViewModel);
    }

    public async Task<IEnumerable<ElectionViewModel>> GetElectionsByTypeAsync(ElectionType? type)
    {
        IEnumerable<Election> elections;
        
        if (type.HasValue)
        {
            elections = await _electionRepository.GetElectionsByTypeAsync(type.Value);
        }
        else
        {
            elections = await _electionRepository.GetOngoingElectionsAsync();
        }

        return elections.Select(MapToViewModel);
    }

    public async Task<ElectionViewModel?> GetElectionByIdAsync(int id)
    {
        var election = await _electionRepository.GetByIdAsync(id);
        return election != null ? MapToViewModel(election) : null;
    }

    public async Task<ElectionViewModel> CreateElectionAsync(ElectionViewModel viewModel)
    {
        var election = new Election
        {
            Title = viewModel.Title,
            Description = viewModel.Description,
            Type = viewModel.Type,
            StartDate = viewModel.StartDate,
            EndDate = viewModel.EndDate,
            IsActive = viewModel.IsActive,
            CreatedAt = DateTime.UtcNow
        };

        var createdElection = await _electionRepository.AddAsync(election);
        return MapToViewModel(createdElection);
    }

    public async Task<ElectionViewModel> UpdateElectionAsync(int id, ElectionViewModel viewModel)
    {
        var election = await _electionRepository.GetByIdAsync(id);
        if (election == null)
            throw new ArgumentException($"Election with id {id} not found", nameof(id));

        election.Title = viewModel.Title;
        election.Description = viewModel.Description;
        election.Type = viewModel.Type;
        election.StartDate = viewModel.StartDate;
        election.EndDate = viewModel.EndDate;
        election.IsActive = viewModel.IsActive;
        election.UpdatedAt = DateTime.UtcNow;

        await _electionRepository.UpdateAsync(election);
        return MapToViewModel(election);
    }

    public async Task DeleteElectionAsync(int id)
    {
        var election = await _electionRepository.GetByIdAsync(id);
        if (election != null)
        {
            await _electionRepository.DeleteAsync(election);
        }
    }

    public async Task<bool> ElectionExistsAsync(int id)
    {
        return await _electionRepository.ExistsAsync(id);
    }

    private ElectionViewModel MapToViewModel(Election election)
    {
        return new ElectionViewModel
        {
            ElectionId = election.ElectionId,
            Title = election.Title,
            Description = election.Description,
            Type = election.Type,
            StartDate = election.StartDate,
            EndDate = election.EndDate,
            IsActive = election.IsActive
        };
    }
}


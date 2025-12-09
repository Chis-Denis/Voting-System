using ASP1.Backend.Domain.Entities;
using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Interfaces;

public interface IElectionService
{
    Task<IEnumerable<ElectionViewModel>> GetOngoingElectionsAsync();
    Task<IEnumerable<ElectionViewModel>> GetElectionsByTypeAsync(ElectionType? type);
    Task<ElectionViewModel?> GetElectionByIdAsync(int id);
    Task<ElectionViewModel> CreateElectionAsync(ElectionViewModel viewModel);
    Task<ElectionViewModel> UpdateElectionAsync(int id, ElectionViewModel viewModel);
    Task DeleteElectionAsync(int id);
    Task<bool> ElectionExistsAsync(int id);
}


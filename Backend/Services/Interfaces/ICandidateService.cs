using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Interfaces;

public interface ICandidateService
{
    Task<IEnumerable<CandidateViewModel>> GetAllCandidatesAsync();
    Task<CandidateViewModel?> GetCandidateByIdAsync(int id);
    Task<CandidateViewModel> CreateCandidateAsync(CandidateViewModel viewModel);
    Task<CandidateViewModel> UpdateCandidateAsync(int id, CandidateViewModel viewModel);
    Task DeleteCandidateAsync(int id);
    Task<bool> CandidateExistsAsync(int id);
}


using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Interfaces;

public interface IVoteService
{
    Task<bool> SubmitVoteAsync(VoteViewModel voteViewModel);
    Task<ElectionDetailsViewModel> GetElectionDetailsAsync(int electionId);
}


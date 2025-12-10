using Microsoft.EntityFrameworkCore;
using ASP1.Backend.Data.Repositories;
using ASP1.Backend.Domain.Entities;
using ASP1.Backend.Services.Interfaces;
using ASP1.Backend.ViewModels;

namespace ASP1.Backend.Services.Implementations;

public class VoteService : IVoteService
{
    private readonly IVoteRepository _voteRepository;
    private readonly IElectionRepository _electionRepository;
    private readonly ICandidateRepository _candidateRepository;
    private readonly ICountyRepository _countyRepository;
    private readonly IElectionCandidateRepository _electionCandidateRepository;
    private readonly IPartyRepository _partyRepository;

    public VoteService(
        IVoteRepository voteRepository,
        IElectionRepository electionRepository,
        ICandidateRepository candidateRepository,
        ICountyRepository countyRepository,
        IElectionCandidateRepository electionCandidateRepository,
        IPartyRepository partyRepository)
    {
        _voteRepository = voteRepository;
        _electionRepository = electionRepository;
        _candidateRepository = candidateRepository;
        _countyRepository = countyRepository;
        _electionCandidateRepository = electionCandidateRepository;
        _partyRepository = partyRepository;
    }

    public async Task<bool> SubmitVoteAsync(VoteViewModel voteViewModel)
    {
        // Verify election exists and is active
        var election = await _electionRepository.GetByIdAsync(voteViewModel.ElectionId);
        if (election == null || !election.IsActive || DateTime.UtcNow < election.StartDate || DateTime.UtcNow > election.EndDate)
        {
            return false;
        }

        // Verify candidate exists and is in this election
        var candidate = await _candidateRepository.GetByIdAsync(voteViewModel.CandidateId);
        if (candidate == null)
        {
            return false;
        }

        var isInElection = await _electionCandidateRepository.IsCandidateInElectionAsync(voteViewModel.ElectionId, voteViewModel.CandidateId);
        if (!isInElection)
        {
            return false;
        }

        // Verify county exists
        var county = await _countyRepository.GetByIdAsync(voteViewModel.CountyId);
        if (county == null)
        {
            return false;
        }

        // Create vote
        var vote = new Vote
        {
            ElectionId = voteViewModel.ElectionId,
            CandidateId = voteViewModel.CandidateId,
            CountyId = voteViewModel.CountyId,
            VotedAt = DateTime.UtcNow
        };

        await _voteRepository.AddAsync(vote);
        return true;
    }

    public async Task<ElectionDetailsViewModel> GetElectionDetailsAsync(int electionId)
    {
        var election = await _electionRepository.GetByIdAsync(electionId);
        if (election == null)
        {
            throw new ArgumentException("Election not found");
        }

        var viewModel = new ElectionDetailsViewModel
        {
            ElectionId = election.ElectionId,
            Title = election.Title,
            Description = election.Description,
            Type = election.Type,
            StartDate = election.StartDate,
            EndDate = election.EndDate,
            IsActive = election.IsActive,
            TypeDisplayName = election.Type.ToString(),
            Status = election.IsActive && DateTime.UtcNow >= election.StartDate && DateTime.UtcNow <= election.EndDate ? "Ongoing" : "Ended"
        };

        // Get all candidates in this election
        var electionCandidates = await _electionCandidateRepository.GetCandidatesByElectionAsync(electionId);
        var allCandidates = electionCandidates.Select(ec => ec.Candidate).ToList();

        // Get total votes for this election
        var totalVotes = await _voteRepository.GetVoteCountByElectionAsync(electionId);
        viewModel.TotalVotes = totalVotes;

        // Get total eligible voters (sum of all counties)
        var totalEligibleVoters = await _countyRepository.GetTotalEligibleVotersAsync();
        viewModel.TotalEligibleVoters = totalEligibleVoters;
        viewModel.VoterTurnoutPercentage = totalEligibleVoters > 0 ? (double)totalVotes / totalEligibleVoters * 100 : 0;

        // Get candidate statistics
        var candidateStats = new List<CandidateVoteViewModel>();
        foreach (var candidate in allCandidates)
        {
            var voteCount = await _voteRepository.GetVoteCountByCandidateAsync(candidate.CandidateId, electionId);
            var votePercentage = totalVotes > 0 ? (double)voteCount / totalVotes * 100 : 0;

            candidateStats.Add(new CandidateVoteViewModel
            {
                CandidateId = candidate.CandidateId,
                Name = candidate.Name,
                PartyName = candidate.Party?.Name ?? "Independent",
                ImageUrl = candidate.ImageUrl,
                Position = candidate.Position,
                VoteCount = voteCount,
                VotePercentage = votePercentage
            });
        }
        viewModel.Candidates = candidateStats.OrderByDescending(c => c.VoteCount).ToList();

        // Get party statistics
        var parties = await _partyRepository.GetAllAsync();
        var partyStats = new List<PartyVoteViewModel>();
        foreach (var party in parties)
        {
            var partyVotes = await _voteRepository.GetVoteCountByPartyAsync(party.PartyId, electionId);
            var partyPercentage = totalVotes > 0 ? (double)partyVotes / totalVotes * 100 : 0;
            var candidateCount = allCandidates.Count(c => c.PartyId == party.PartyId);

            partyStats.Add(new PartyVoteViewModel
            {
                PartyId = party.PartyId,
                PartyName = party.Name,
                TotalVotes = partyVotes,
                VotePercentage = partyPercentage,
                CandidateCount = candidateCount
            });
        }
        viewModel.PartyStatistics = partyStats.OrderByDescending(p => p.TotalVotes).ToList();

        // Get county statistics
        var counties = await _countyRepository.GetAllCountiesAsync();
        var countyStats = new List<CountyVoteViewModel>();
        foreach (var county in counties)
        {
            var countyVotes = await _voteRepository.GetVoteCountByCountyAsync(county.CountyId, electionId);
            var turnoutPercentage = county.EligibleVoters > 0 ? (double)countyVotes / county.EligibleVoters * 100 : 0;

            countyStats.Add(new CountyVoteViewModel
            {
                CountyId = county.CountyId,
                CountyName = county.Name,
                CountyCode = county.Code,
                VotesCast = countyVotes,
                EligibleVoters = county.EligibleVoters,
                TurnoutPercentage = turnoutPercentage
            });
        }
        viewModel.CountyStatistics = countyStats.OrderByDescending(c => c.VotesCast).ToList();

        return viewModel;
    }
}



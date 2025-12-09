using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.ViewModels;

public class ElectionDetailsViewModel
{
    public int ElectionId { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ElectionType Type { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsActive { get; set; }
    public string TypeDisplayName { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    // Statistics
    public int TotalVotes { get; set; }
    public int TotalEligibleVoters { get; set; }
    public double VoterTurnoutPercentage { get; set; }

    // Candidates with votes
    public List<CandidateVoteViewModel> Candidates { get; set; } = new();

    // Party statistics
    public List<PartyVoteViewModel> PartyStatistics { get; set; } = new();

    // County statistics
    public List<CountyVoteViewModel> CountyStatistics { get; set; } = new();
}

public class CandidateVoteViewModel
{
    public int CandidateId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PartyName { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public string Position { get; set; } = string.Empty;
    public int VoteCount { get; set; }
    public double VotePercentage { get; set; }
}

public class PartyVoteViewModel
{
    public int PartyId { get; set; }
    public string PartyName { get; set; } = string.Empty;
    public int TotalVotes { get; set; }
    public double VotePercentage { get; set; }
    public int CandidateCount { get; set; }
}

public class CountyVoteViewModel
{
    public int CountyId { get; set; }
    public string CountyName { get; set; } = string.Empty;
    public string CountyCode { get; set; } = string.Empty;
    public int VotesCast { get; set; }
    public int EligibleVoters { get; set; }
    public double TurnoutPercentage { get; set; }
}


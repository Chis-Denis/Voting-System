namespace ASP1.Backend.ViewModels;

public class StatisticsViewModel
{
    public int TotalCandidates { get; set; }
    public int TotalParties { get; set; }
    public List<PartyStatsViewModel> CandidatesPerParty { get; set; } = new();
}

public class PartyStatsViewModel
{
    public string PartyName { get; set; } = string.Empty;
    public int CandidateCount { get; set; }
}


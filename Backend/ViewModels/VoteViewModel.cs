using System.ComponentModel.DataAnnotations;

namespace ASP1.Backend.ViewModels;

public class VoteViewModel
{
    [Required]
    public int ElectionId { get; set; }

    [Required]
    public int CandidateId { get; set; }

    [Required]
    public int CountyId { get; set; }
}


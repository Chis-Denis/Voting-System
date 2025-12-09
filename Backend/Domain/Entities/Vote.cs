using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP1.Backend.Domain.Entities;

public class Vote
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int VoteId { get; set; }

    [Required]
    public int ElectionId { get; set; }

    [Required]
    public int CandidateId { get; set; }

    [Required]
    public int CountyId { get; set; }

    public DateTime VotedAt { get; set; } = DateTime.UtcNow;

    // Navigation properties
    public Election Election { get; set; } = null!;
    public Candidate Candidate { get; set; } = null!;
    public County County { get; set; } = null!;
}


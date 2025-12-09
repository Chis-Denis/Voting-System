using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP1.Backend.Domain.Entities;

public class ElectionCandidate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ElectionCandidateId { get; set; }

    [Required]
    public int ElectionId { get; set; }

    [Required]
    public int CandidateId { get; set; }

    // Navigation properties
    public Election Election { get; set; } = null!;
    public Candidate Candidate { get; set; } = null!;
}


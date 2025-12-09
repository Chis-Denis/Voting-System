using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP1.Backend.Domain.Entities;

public enum ElectionType
{
    Local = 1,
    Parliamentarian = 2,
    Presidential = 3,
    Referendum = 4
}

public class Election
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int ElectionId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    [Required]
    public ElectionType Type { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? UpdatedAt { get; set; }

    // Navigation properties
    public ICollection<ElectionCandidate> ElectionCandidates { get; set; } = new List<ElectionCandidate>();
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}


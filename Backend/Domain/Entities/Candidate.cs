using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP1.Backend.Domain.Entities;

public class Candidate
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CandidateId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public string ImageUrl { get; set; } = string.Empty;
    
    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120.")]
    public int Age { get; set; }
    
    [Required]
    public string Position { get; set; } = string.Empty;
    
    public int? PartyId { get; set; }
    
    public Party? Party { get; set; }

    // Navigation properties
    public ICollection<ElectionCandidate> ElectionCandidates { get; set; } = new List<ElectionCandidate>();
    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}


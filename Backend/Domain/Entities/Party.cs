using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP1.Backend.Domain.Entities;

public class Party
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int PartyId { get; set; }
    
    [Required]
    public string Name { get; set; } = string.Empty;
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    public string LogoUrl { get; set; } = string.Empty;
    
    public ICollection<Candidate> Candidates { get; set; } = new List<Candidate>();
}


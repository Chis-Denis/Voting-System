using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASP1.Backend.Domain.Entities;

public class County
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int CountyId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [StringLength(10)]
    public string Code { get; set; } = string.Empty; // e.g., "AB" for Alba

    public int EligibleVoters { get; set; } = 0; // Total eligible voters in this county

    public ICollection<Vote> Votes { get; set; } = new List<Vote>();
}



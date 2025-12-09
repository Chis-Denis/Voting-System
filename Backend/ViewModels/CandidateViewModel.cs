using System.ComponentModel.DataAnnotations;

namespace ASP1.Backend.ViewModels;

public class CandidateViewModel
{
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

    public string PartyName { get; set; } = string.Empty;
}


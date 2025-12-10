using System.ComponentModel.DataAnnotations;
using ASP1.Backend.Domain.Entities;

namespace ASP1.Backend.ViewModels;

public class ElectionViewModel
{
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
    [DataType(DataType.Date)]
    public DateTime StartDate { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndDate { get; set; }

    public bool IsActive { get; set; }

    public string TypeDisplayName => Type switch
    {
        ElectionType.Local => "Local Elections",
        ElectionType.Parliamentarian => "Parliamentarian Elections",
        ElectionType.Presidential => "Presidential Elections",
        ElectionType.Referendum => "Referendum",
        _ => Type.ToString()
    };

    public string Status => IsActive && DateTime.UtcNow >= StartDate && DateTime.UtcNow <= EndDate 
        ? "Ongoing" 
        : DateTime.UtcNow < StartDate 
            ? "Upcoming" 
            : "Ended";
}



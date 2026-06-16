using System.ComponentModel.DataAnnotations;

namespace SaasClinicas.Api.Dtos.Professionals;

public class ProfessionalUpdateDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    [Required]
    [Range(0.0, 5000.00, MinimumIsExclusive = false, MaximumIsExclusive = true)]
    public decimal SessionPrice { get; set; }
}
using System.ComponentModel.DataAnnotations;

namespace SaasClinicas.Api.Dtos.Professionals;

public class ProfessionalResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    public decimal SessionPrice { get; set; }
    public int ClinicId { get; set; }
    public DateTime CreatedAt { get; set; }
}
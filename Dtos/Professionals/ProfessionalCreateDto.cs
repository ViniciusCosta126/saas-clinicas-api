namespace SaasClinicas.Api.Dtos.Professionals;

public class ProfessionalCreateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    public decimal SessionPrice { get; set; }
    public int ClinicId { get; set; }
}
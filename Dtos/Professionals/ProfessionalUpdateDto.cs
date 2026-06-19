namespace SaasClinicas.Api.Dtos.Professionals;

public class ProfessionalUpdateDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    public decimal SessionPrice { get; set; }
}
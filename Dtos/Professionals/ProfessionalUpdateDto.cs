using SaasClinicas.Api.Dtos.Base;

namespace SaasClinicas.Api.Dtos.Professionals;

public class ProfessionalUpdateDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    public decimal SessionPrice { get; set; }
}
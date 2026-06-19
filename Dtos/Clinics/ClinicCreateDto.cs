namespace SaasClinicas.Api.Dtos.Clinics;
public class ClinicCreateDto
{
    public string ClinicName { get; set; } = string.Empty;
    public string ResponsibleName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }

}
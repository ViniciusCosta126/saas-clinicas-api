namespace SaasClinicas.APi.Dtos.Clinics;

public class ClinicResponseDto
{
    public int Id { get; set; }
    public string ClinicName { get; set; } = string.Empty;
    public string ResponsibleName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
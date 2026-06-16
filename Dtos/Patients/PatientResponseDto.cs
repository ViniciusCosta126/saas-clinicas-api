using System.ComponentModel.DataAnnotations;

namespace SaasClinicas.Api.Dtos.Patients;


public class PatientResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
    public string Cpf { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
}
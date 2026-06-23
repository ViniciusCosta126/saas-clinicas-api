using System.ComponentModel.DataAnnotations;
using SaasClinicas.Api.Dtos.Base;

namespace SaasClinicas.Api.Dtos.Patients;


public class PatientUpdateDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public DateOnly Birthday { get; set; }
    public string Cpf { get; set; } = string.Empty;
}
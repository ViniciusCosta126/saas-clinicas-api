using System.ComponentModel.DataAnnotations;

namespace SaasClinicas.APi.Dtos.Clinics;


public class ClinicUpdateDto
{
    [Required(ErrorMessage = "O nome da clinica é obrigatorio.")]
    [StringLength(255)]
    public string ClinicName { get; set; } = string.Empty;
    [Required(ErrorMessage = "O nome do responsavel é obrigatorio.")]
    [StringLength(255)]
    public string ResponsibleName { get; set; } = string.Empty;
    [Required(ErrorMessage = "O e-mail é obrigatório.")]
    [EmailAddress(ErrorMessage = "E-mail inválido.")]
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }

}
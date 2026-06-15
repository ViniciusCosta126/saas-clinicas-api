using System.ComponentModel.DataAnnotations;
using SaasClinicas.Api.Models.Base;

namespace SaasClinicas.Api.Models;

public class Clinic : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "O nome da clinica é obrigatorio.")]
    [StringLength(255)]
    public string ClinicName { get; set; } = string.Empty;
    [Required(ErrorMessage = "O nome do responsavel é obrigatorio.")]
    [StringLength(255)]
    public string ResponsibleName { get; set; } = string.Empty;
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string? Phone { get; set; }

    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Professional> Professionals { get; set; } = new List<Professional>();
    public ICollection<ClinicPatient> ClinicPatients { get; set; }
    = new List<ClinicPatient>();

}
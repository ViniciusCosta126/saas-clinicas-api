using System.ComponentModel.DataAnnotations;
using SaasClinicas.Api.Models.Base;

namespace SaasClinicas.Api.Models;

public class Patient : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [StringLength(255, MinimumLength = 4)]
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [RegularExpression(@"^\d{10,11}$")]
    [Required]
    public string Phone { get; set; } = string.Empty;
    [Required]
    public DateOnly Birthday { get; set; }
    [Required]
    [RegularExpression(@"^\d{11}$")]
    public string Cpf { get; set; } = string.Empty;
    public ICollection<ClinicPatient> ClinicPatients { get; set; }
       = new List<ClinicPatient>();
}
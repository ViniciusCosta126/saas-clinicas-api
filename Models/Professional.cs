using System.ComponentModel.DataAnnotations;
using SaasClinicas.Api.Models.Base;

namespace SaasClinicas.Api.Models;


public class Professional : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    public decimal SessionPrice { get; set; }

    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; } = null!;

}
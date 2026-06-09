using System.ComponentModel.DataAnnotations;
using SaasClinicas.APi.Models.Base;

namespace SaasClinicas.APi.Models;


public class Professional : BaseEntity
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    public string? Specialty { get; set; }
    [Required]
    [Range(0.0, 5000.00, MinimumIsExclusive = false, MaximumIsExclusive = true)]
    public decimal SessionPrice { get; set; }

    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; } = null!;

}
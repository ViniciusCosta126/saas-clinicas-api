using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using SaasClinicas.APi.Enums;

namespace SaasClinicas.APi.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;
    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;
    [RegularExpression(@"^\d{10,11}$")]
    public string Phone { get; set; } = string.Empty;
    [Required]
    [RegularExpression(@"^\d{11}$", ErrorMessage = "CPF deve conter 11 números")]
    public string Cpf { get; set; } = string.Empty;
    public DateTime? EmailVerifiedAt { get; set; }
    [Required]
    public string Password { get; set; } = string.Empty;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    public UserRole Role { get; set; }

    public int ClinicId { get; set; }
    public Clinic Clinic { get; set; } = null!;

}
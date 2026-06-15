using System.ComponentModel.DataAnnotations;
using SaasClinicas.Api.Enums;

namespace SaasClinicas.Api.Dtos.Users;

public class UserUpdateDto
{
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
    [Required]
    public string Password { get; set; } = string.Empty;
    [Required]
    public UserRole Role { get; set; }
}
using System.ComponentModel.DataAnnotations;
using SaasClinicas.Api.Dtos.Clinics;
using SaasClinicas.Api.Dtos.Users;

namespace SaasClinicas.Api.Dtos.Auth;

public class RegisterDto
{
    public ClinicCreateDto Clinic { get; set; }

    public UserRegisterDto User { get; set; }
}

public class UserRegisterDto
{
    [Required]
    [StringLength(255)]
    public string Name { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    public string Email { get; set; } = string.Empty;

    [RegularExpression(@"^\d{10,11}$")]
    [Required]
    public string Phone { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^\d{11}$")]
    public string Cpf { get; set; } = string.Empty;

    [Required]
    [StringLength(255, MinimumLength = 6)]
    public string Password { get; set; } = string.Empty;
}
using SaasClinicas.Api.Dtos.Base;
using SaasClinicas.Api.Enums;

namespace SaasClinicas.Api.Dtos.Users;

public class UserUpdateDto : BaseDto
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Cpf { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public UserRole Role { get; set; }
}
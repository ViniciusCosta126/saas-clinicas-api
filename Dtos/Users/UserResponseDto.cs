using SaasClinicas.Api.Enums;

namespace SaasClinicas.Api.Dtos.Users;

public class UserResponseDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;

    public string Cpf { get; set; } = string.Empty;
    public UserRole Role { get; set; }
    public int ClinicId { get; set; }
    public DateTime CreatedAt { get; set; }

}
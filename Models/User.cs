using System.ComponentModel.DataAnnotations;

namespace SaasClinicas.APi.Models;

public class User
{
    [Key]
    public int Id { get; set; }
    [Required]
    [StringLength(255)]
    public string Name { get; set; }
    [Required]
    [EmailAddress]
    public string Email { get; set; }
    [StringLength(11)]
    public string Phone { get; set; }
    [Required]
    [StringLength(11)]
    public string Cpf { get; set; }
    public DateTime? vemail_verified_at { get; set; }
    [Required]
    public string? Password { get; set; }

    public DateTime CreatedAt = DateTime.UtcNow;
    public DateTime UpdatedAt = DateTime.UtcNow;
    public DateTime? DeletedAt { get; set; }

    [StringLength(50)]
    [MinLength(3)]
    public string? Role { get; set; }
}
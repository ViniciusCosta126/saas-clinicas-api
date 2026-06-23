using System.ComponentModel.DataAnnotations;
using SaasClinicas.Api.Models.Base;

namespace SaasClinicas.Api.Models;

public class Clinic : BaseEntity
{
    [Key]
    public int Id { get; set; }
    public string ClinicName { get; set; } = string.Empty;
    public string ResponsibleName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public ICollection<User> Users { get; set; } = new List<User>();
    public ICollection<Professional> Professionals { get; set; } = new List<Professional>();
    public ICollection<ClinicPatient> ClinicPatients { get; set; }
    = new List<ClinicPatient>();

}
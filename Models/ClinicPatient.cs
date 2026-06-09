namespace SaasClinicas.APi.Models;

public class ClinicPatient
{
    public int ClinicId {get;set;}
    public Clinic Clinic {get;set;} = null!;

    public int PatientId {get;set;}
    public Patient Patient {get;set;} = null!;
    public DateTime RegistrationDate { get; set; }
}
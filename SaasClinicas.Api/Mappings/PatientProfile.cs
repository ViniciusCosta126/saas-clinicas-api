using AutoMapper;
using SaasClinicas.Api.Dtos.Patients;
using SaasClinicas.Api.Models;

namespace SaasClinicas.Api.Mappings;


public class PatientProfile : Profile
{
    public PatientProfile()
    {
        CreateMap<Patient, PatientResponseDto>();
        CreateMap<PatientCreateDto, Patient>();
        CreateMap<PatientUpdateDto, Patient>();
    }
}
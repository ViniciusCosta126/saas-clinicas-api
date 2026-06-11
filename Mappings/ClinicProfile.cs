
using AutoMapper;
using SaasClinicas.APi.Dtos.Clinics;
using SaasClinicas.APi.Models;

namespace SaasClinicas.APi.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Clinic, ClinicResponseDto>();
        CreateMap<ClinicCreateDto, Clinic>();
        CreateMap<ClinicUpdateDto, Clinic>();
    }
}
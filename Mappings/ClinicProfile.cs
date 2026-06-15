
using AutoMapper;
using SaasClinicas.Api.Dtos.Clinics;
using SaasClinicas.Api.Models;

namespace SaasClinicas.Api.Mappings;

public class ClinicProfile : Profile
{
    public ClinicProfile()
    {
        CreateMap<Clinic, ClinicResponseDto>();
        CreateMap<ClinicCreateDto, Clinic>();
        CreateMap<ClinicUpdateDto, Clinic>();
    }
}
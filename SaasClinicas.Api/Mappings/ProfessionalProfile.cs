using AutoMapper;
using SaasClinicas.Api.Dtos.Professionals;
using SaasClinicas.Api.Models;

namespace SaasClinicas.Api.Mappings;

public class ProfessionalProfile : Profile
{
    public ProfessionalProfile()
    {
        CreateMap<Professional, ProfessionalResponseDto>();
        CreateMap<ProfessionalCreateDto, Professional>();
        CreateMap<ProfessionalUpdateDto, Professional>();
    }
}
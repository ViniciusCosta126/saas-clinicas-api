using AutoMapper;
using SaasClinicas.Api.Dtos.Users;
using SaasClinicas.Api.Models;

namespace SaasClinicas.Api.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserResponseDto>();
        CreateMap<UserCreateDto, User>();
        CreateMap<UserUpdateDto, User>();
    }
}
using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Services.Mappings;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserProfileDto, User>();
        CreateMap<UserLoginDto, User>();
    }
}
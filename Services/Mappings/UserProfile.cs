using AutoMapper;
using Entities.Dtos;
using Entities.Models;

namespace Services.Contracts;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<UserProfileDto, User>();
        CreateMap<UserLoginDto, User>();
    }
}
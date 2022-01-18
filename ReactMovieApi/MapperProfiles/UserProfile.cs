using AutoMapper;
using Microsoft.AspNetCore.Identity;
using ReactMovieApi.DTOs.UserDTOs;

namespace ReactMovieApi.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserReadDto>();
        }
    }
}

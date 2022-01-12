using AutoMapper;
using ReactMovieApi.DTOs.ActorDTOs;
using ReactMovieApi.Models;

namespace ReactMovieApi.MapperProfiles
{
    public class ActorProfile : Profile
    {
        public ActorProfile()
        {
            CreateMap<ActorCreateDto, Actor>();
            CreateMap<Actor, ActorReadDto>();
        }
    }
}

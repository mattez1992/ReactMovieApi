using AutoMapper;
using ReactMovieApi.DTOs;
using ReactMovieApi.Models;

namespace ReactMovieApi.MapperProfiles
{
    public class MovieActorProfile : Profile
    {
        public MovieActorProfile()
        {
            CreateMap<MoviesActors, MovieActorReadDto>().ReverseMap();
        }
    }
}

using AutoMapper;
using ReactMovieApi.DTOs;
using ReactMovieApi.Models;

namespace ReactMovieApi.MapperProfiles
{
    public class GenreProfiles : Profile
    {
        public GenreProfiles()
        {
            CreateMap<GenreCreationDto, Genre>();
            CreateMap<Genre, GenreReadDto>();
        }
    }
}

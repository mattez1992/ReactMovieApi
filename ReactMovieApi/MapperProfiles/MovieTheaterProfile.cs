using AutoMapper;
using NetTopologySuite.Geometries;
using ReactMovieApi.DTOs.MovieTheaterDTOs;
using ReactMovieApi.Models;

namespace ReactMovieApi.MapperProfiles
{
    public class MovieTheaterProfile : Profile
    {
        public MovieTheaterProfile(GeometryFactory geometryFactory)
        {
            CreateMap<MovieTheaterCreateDto, MovieTheater>()
                .ForMember(x => x.Location, x => x.MapFrom(dto => geometryFactory.CreatePoint(new Coordinate(dto.Longitude, dto.Latitude))));
            CreateMap<MovieTheater, MovieTheaterReadDto>()
                .ForMember(x => x.Latitude, dto => dto.MapFrom(prop => prop.Location.Y))
                .ForMember(x => x.Longitude, dto => dto.MapFrom(prop => prop.Location.X));
        }
    }
}

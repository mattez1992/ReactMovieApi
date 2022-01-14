using AutoMapper;
using ReactMovieApi.DTOs;
using ReactMovieApi.DTOs.MovieDtos;
using ReactMovieApi.DTOs.MovieTheaterDTOs;
using ReactMovieApi.Models;

namespace ReactMovieApi.MapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(x => x.Poster, opt => opt.Ignore())
                .ForMember(x => x.Genres, opt => opt.MapFrom(MapGenres))
                .ForMember(x => x.MovieTheaters, opt => opt.MapFrom(MapTheaters))
                .ForMember(x => x.MoviesActors, opt => opt.MapFrom(MapMovieActors));
            CreateMap<Movie, MovieReadDto>()
                .ForMember(x => x.Genres, opt => opt.MapFrom(MapMoviesGenres))
                .ForMember(x => x.MovieTheaters, opt => opt.MapFrom(MapMovieTheatersMovies))
                .ForMember(x => x.MoviesActors, opt => opt.MapFrom(MapMoviesActors));
        }
        #region Mapping For Reading
        // mapppings for reading
        private IList<MovieActorReadDto> MapMoviesActors(Movie movie, MovieReadDto readDto)
        {
            var result = new List<MovieActorReadDto>();

            for (int i = 0; i < movie.MoviesActors.Count; i++)
            {
                result.Add(new MovieActorReadDto
                {
                    ActorId = movie.MoviesActors[i].ActorId,
                    Name = movie.MoviesActors[i].Actor.Name,
                    Picture = movie.MoviesActors[i].Actor.Picture,
                    Character = movie.MoviesActors[i].Character,
                    Order = movie.MoviesActors[i].Order
                });
            }

            return result;
        }
        private IList<MovieTheaterReadDto> MapMovieTheatersMovies(Movie movie, MovieReadDto readDto)
        {

            var theaters = new List<MovieTheaterReadDto>();

            if (movie.MovieTheaters != null)
            {
                foreach (var theater in movie.MovieTheaters)
                {
                    theaters.Add(new MovieTheaterReadDto() { Id = theater.Id, Latitude = theater.Location.Coordinate.Y, Longitude = theater.Location.Coordinate.X, Name = theater.Name });
                }
            }

            return theaters;
        }
        private IList<GenreReadDto> MapMoviesGenres(Movie movie, MovieReadDto readDto)
        {
            var genres = new List<GenreReadDto>();

            if (movie.Genres != null)
            {
                foreach (var genre in movie.Genres)
                {
                    genres.Add(new GenreReadDto() { Id = genre.Id, Name = genre.Name });
                }
            }

            return genres;
        }
        #endregion

        #region Mappings For Creating A Movie
        // mappings for creating
        private IList<Genre> MapGenres(MovieCreateDto createDto, Movie movie)
        {
            var result = new List<Genre>();
            if (createDto.GenresIds == null)
            {
                return result;
            }

            foreach (var id in createDto.GenresIds)
            {
                result.Add(new Genre { Id = id });
            }
            return result;
        }
        private IList<MovieTheater> MapTheaters(MovieCreateDto createDto, Movie movie)
        {
            var result = new List<MovieTheater>();
            if (createDto.GenresIds == null)
            {
                return result;
            }

            foreach (var id in createDto.MovieTheatersIds)
            {
                result.Add(new MovieTheater { Id = id });
            }
            return result;
        }
        private IList<MoviesActors> MapMovieActors(MovieCreateDto createDto, Movie movie)
        {
            var result = new List<MoviesActors>();
            if (createDto.Actors == null)
            {
                return result;
            }
            //for (int i = 0; i < createDto.Actors.Count; i++)
            //{
            //    result.Add(new MoviesActors { ActorId = createDto.Actors[i].Id, Character = createDto.Actors[i].MovieCharacter, Order = i+1 });
            //}
            foreach (var actor in createDto.Actors)
            {
                result.Add(new MoviesActors { ActorId = actor.Id, Character = actor.MovieCharacter });
            }
            return result;
        }
    }
    #endregion

}

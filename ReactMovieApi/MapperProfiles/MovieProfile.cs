using AutoMapper;
using ReactMovieApi.DTOs;
using ReactMovieApi.DTOs.MovieDtos;
using ReactMovieApi.DTOs.MovieTheaterDTOs;
using ReactMovieApi.Models;
using ReactMovieApi.Models.LinkingTables;

namespace ReactMovieApi.MapperProfiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieCreateDto, Movie>()
                .ForMember(x => x.Poster, opt => opt.Ignore())
                .ForMember(x => x.MoviesGenres, opt => opt.MapFrom(MapGenres))
                .ForMember(x => x.MovieTheatersMovies, opt => opt.MapFrom(MapTheaters))
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
            if(movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    result.Add(new MovieActorReadDto
                    {
                        Id = movie.MoviesActors[i].ActorId,
                        Name = movie.MoviesActors[i].Actor.Name,
                        Picture = movie.MoviesActors[i].Actor.Picture,
                        MovieCharacter = movie.MoviesActors[i].MovieCharacter,
                        Order = movie.MoviesActors[i].Order
                    });
                }
            }
            

            return result;
        }
        private IList<MovieTheaterReadDto> MapMovieTheatersMovies(Movie movie, MovieReadDto readDto)
        {

            var theaters = new List<MovieTheaterReadDto>();

            if (movie.MovieTheatersMovies != null)
            {
                foreach (var theater in movie.MovieTheatersMovies)
                {
                    theaters.Add(new MovieTheaterReadDto()
                    {
                        Id = theater.MovieTheaterId,
                        Name = theater.MovieTheater.Name,
                        Latitude = theater.MovieTheater.Location.Y,
                        Longitude = theater.MovieTheater.Location.X
                    });
                }
                }

            return theaters;
        }
        private IList<GenreReadDto> MapMoviesGenres(Movie movie, MovieReadDto readDto)
        {
            var genres = new List<GenreReadDto>();

            if (movie.MoviesGenres != null)
            {
                foreach (var genre in movie.MoviesGenres)
                {
                    genres.Add(new GenreReadDto() { Id = genre.GenreId, Name = genre.Genre.Name });
                }
            }

            return genres;
        }
        #endregion

        #region Mappings For Creating A Movie
        // mappings for creating
        private IList<GenreMovie> MapGenres(MovieCreateDto createDto, Movie movie)
        {
            var result = new List<GenreMovie>();
            if (createDto.GenresIds == null)
            {
                return result;
            }

            foreach (var id in createDto.GenresIds)
            {
                result.Add(new GenreMovie { GenreId = id });
            }
            return result;
        }
        private IList<MovieMovieTheater> MapTheaters(MovieCreateDto createDto, Movie movie)
        {
            var result = new List<MovieMovieTheater>();
            if (createDto.GenresIds == null)
            {
                return result;
            }

            foreach (var id in createDto.MovieTheatersIds)
            {
                result.Add(new MovieMovieTheater { MovieTheaterId = id });
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
            foreach (var actor in createDto.Actors)
            {
                result.Add(new MoviesActors { ActorId = actor.Id, MovieCharacter = actor.MovieCharacter });
            }
            return result;
        }
    }
    #endregion

}

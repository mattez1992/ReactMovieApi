using ReactMovieApi.DTOs.MovieTheaterDTOs;

namespace ReactMovieApi.DTOs.MovieDtos
{
    public class MovieEditPageDto
    {
        public MovieReadDto Movie { get; set; }
        public List<GenreReadDto> SelectedGenres { get; set; }
        public List<GenreReadDto> NonSelectedGenres { get; set; }
        public List<MovieTheaterReadDto> SelectedMovieTheaters { get; set; }
        public List<MovieTheaterReadDto> NonSelectedMovieTheaters { get; set; }
        public List<MovieActorReadDto> Actors { get; set; }

    }
}

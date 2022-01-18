using ReactMovieApi.DTOs.MovieTheaterDTOs;
using ReactMovieApi.Models;

namespace ReactMovieApi.DTOs.MovieDtos
{
    public class MovieReadDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool inTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public virtual List<GenreReadDto> Genres { get; set; }
        public virtual List<MovieTheaterReadDto> MovieTheaters { get; set; }
        public virtual List<MovieActorReadDto> MoviesActors { get; set; }
        public double AverageRating { get; set; }
        public int UserRate { get; set; }
    }
}

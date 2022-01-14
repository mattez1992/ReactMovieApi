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
        public DateTime realseDate { get; set; }
        public string Poster { get; set; }
        public virtual IList<GenreReadDto> Genres { get; set; }
        public virtual IList<MovieTheaterReadDto> MovieTheaters { get; set; }
        public virtual IList<MovieActorReadDto> MoviesActors { get; set; }
    }
}

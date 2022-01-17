using ReactMovieApi.Models.LinkingTables;
using System.ComponentModel.DataAnnotations;
namespace ReactMovieApi.Models
{
    public class Movie :  BaseDbObject
    {
        [StringLength(maximumLength: 100)]
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool inTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public string Poster { get; set; }
        public List<GenreMovie> MoviesGenres { get; set; }
        public List<MovieMovieTheater> MovieTheatersMovies { get; set; }
        public List<MoviesActors> MoviesActors { get; set; }
        //public  List<Genre> Genres { get; set; }
        //public  List<MovieTheater> MovieTheaters { get; set; }
        //public  List<MoviesActors> MoviesActors { get; set; }
    }
}

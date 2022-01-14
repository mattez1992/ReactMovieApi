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
        public virtual IList<Genre> Genres { get; set; }
        public virtual IList<MovieTheater> MovieTheaters { get; set; }
        public virtual IList<MoviesActors> MoviesActors { get; set; }
    }
}

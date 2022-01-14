using Microsoft.AspNetCore.Mvc;
using ReactMovieApi.DTOs.MovieActorsDTOs;
using ReactMovieApi.Utils;
using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.DTOs.MovieDtos
{
    public class MovieCreateDto
    {
        [StringLength(maximumLength: 100)]
        [Required]
        public string Title { get; set; }
        public string Summary { get; set; }
        public string Trailer { get; set; }
        public bool InTheaters { get; set; }
        public DateTime ReleaseDate { get; set; }
        public IFormFile Poster { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GenresIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> MovieTheatersIds { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<MovieActorsCreationDto>>))]
        public List<MovieActorsCreationDto> Actors { get; set; }

        //public virtual IList<Genre> Genres { get; set; }
        //public virtual IList<MovieTheater> MovieTheaters { get; set; }
        //public virtual IList<MoviesActors> MoviesActors { get; set; }
    }
}

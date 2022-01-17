using ReactMovieApi.DTOs.MovieDtos;
using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task<List<Movie>> FilterMovies(HttpContext httpContext, FilterMoviesDto filterMoviesDto);
        void RemoveRelatedActors(Movie movie);
        void RemoveRelatedTheater(Movie movie);
        void RemoveRelatedGenres(Movie movie);
        Task InsertMovie(Movie movie);
    }
}

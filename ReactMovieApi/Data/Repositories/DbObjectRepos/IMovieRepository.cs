using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public interface IMovieRepository : IGenericRepository<Movie>
    {
        Task InsertMovie(Movie movie);
    }
}

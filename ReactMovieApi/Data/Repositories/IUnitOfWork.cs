using ReactMovieApi.Data.Repositories.DbObjectRepos;

namespace ReactMovieApi.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository Genres { get; }
        Task<int> SaveChanges();
    }
}

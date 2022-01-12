using ReactMovieApi.Data.Repositories.DbObjectRepos;

namespace ReactMovieApi.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository Genres { get; }
        IActorRepository Actors { get; }
        Task<int> SaveChanges();
    }
}

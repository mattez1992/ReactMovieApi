using ReactMovieApi.Data.Repositories.DbObjectRepos;

namespace ReactMovieApi.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IGenreRepository Genres { get; }
        IActorRepository Actors { get; }
        IMovieTheaterRepository MovieTheaters { get; }
        IMovieRepository Movies { get; }
        IMovieActorsRepository MovieActors { get; }
        IRatingRepository Ratings { get; }
        IUserRepository Users { get; }
        Task<int> SaveChanges();
    }
}

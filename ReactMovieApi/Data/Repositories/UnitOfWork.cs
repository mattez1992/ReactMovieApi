using ReactMovieApi.Data.Repositories.DbObjectRepos;

namespace ReactMovieApi.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IGenreRepository Genres {get; private set;}

        public IActorRepository Actors { get; private set; }

        public IMovieTheaterRepository MovieTheaters { get; private set; }

        public IMovieRepository Movies { get; private set; }

        public IMovieActorsRepository MovieActors { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            Genres = new GenreRepository(dbContext);
            Actors = new ActorRepository(dbContext);
            MovieTheaters = new MovieTheaterRepository(dbContext);
            Movies = new MovieRepository(dbContext);
            MovieActors = new MovieActorsRepository(dbContext);
            _dbContext = dbContext;
        }
        public void Dispose()
        {
            _dbContext.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<int> SaveChanges()
        {
            return await _dbContext.SaveChangesAsync();
        }
    }
}

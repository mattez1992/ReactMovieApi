using ReactMovieApi.Data.Repositories.DbObjectRepos;

namespace ReactMovieApi.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public IGenreRepository Genres {get; private set;}

        public IActorRepository Actors { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            Genres = new GenreRepository(dbContext);
            Actors = new ActorRepository(dbContext);
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

using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public class MovieActorsRepository : GenericRepository<MoviesActors>, IMovieActorsRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return _dbContext; }
        }
        public MovieActorsRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

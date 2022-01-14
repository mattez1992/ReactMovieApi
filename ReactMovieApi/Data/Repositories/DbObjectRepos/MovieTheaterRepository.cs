using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public class MovieTheaterRepository : GenericRepository<MovieTheater>, IMovieTheaterRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return _dbContext; }
        }
        public MovieTheaterRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

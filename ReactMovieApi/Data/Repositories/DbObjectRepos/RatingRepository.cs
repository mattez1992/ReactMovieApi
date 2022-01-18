using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public class RatingRepository : GenericRepository<Rating>,  IRatingRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return _dbContext; }
        }
        public RatingRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

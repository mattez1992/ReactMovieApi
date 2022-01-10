using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public class GenreRepository : GenericRepository<Genre>, IGenreRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return _dbContext; }
        }
        public GenreRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

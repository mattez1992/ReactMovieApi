using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public class ActorRepository :GenericRepository<Actor>, IActorRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return _dbContext; }
        }
        public ActorRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

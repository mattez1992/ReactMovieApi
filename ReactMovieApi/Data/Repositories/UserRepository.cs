using Microsoft.AspNetCore.Identity;

namespace ReactMovieApi.Data.Repositories
{
    public class UserRepository : GenericRepository<IdentityUser>, IUserRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return _dbContext; }
        }
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}

using Microsoft.AspNetCore.Identity;

namespace ReactMovieApi.Data.Repositories
{
    public interface IUserRepository : IGenericRepository<IdentityUser>
    {
    }
}

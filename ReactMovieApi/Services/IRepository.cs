using ReactMovieApi.Models;

namespace ReactMovieApi.Services
{
    public interface IRepository
    {
        Task<List<Genre>> GetAllGenres();
        Task<Genre?> Get(int id);
        void AddGenre(Genre genre);
    }
}

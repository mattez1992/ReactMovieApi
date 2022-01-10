using ReactMovieApi.Models;

namespace ReactMovieApi.Services
{
    public class InMemoryRepository : IRepository
    {
        private List<Genre> _genreList;
        public InMemoryRepository()
        {
            _genreList = new List<Genre>()
            {
                new Genre(){Id = 1, Name = "Comedy"},
                new Genre(){Id = 2, Name ="Action"}
            };
        }

        public async Task<List<Genre>> GetAllGenres()
        {
            return _genreList;
        }

        public void AddGenre(Genre genre)
        {
            _genreList.Add(genre);
        }

        public async Task<Genre?> Get(int id)
        {
            return _genreList.FirstOrDefault(x => x.Id == id);
        }
    }
}

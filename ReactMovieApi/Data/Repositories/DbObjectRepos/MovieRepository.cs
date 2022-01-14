using ReactMovieApi.Models;

namespace ReactMovieApi.Data.Repositories.DbObjectRepos
{
    public class MovieRepository : GenericRepository<Movie>, IMovieRepository
    {
        public ApplicationDbContext DbContext
        {
            get { return _dbContext; }
        }
        public MovieRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }

        public async Task InsertMovie(Movie movie)
        {
            foreach (var item in movie.Genres)
            {
                _dbContext.Genres.Attach(item);
            }
            foreach (var item in movie.MoviesActors)
            {
                _dbContext.MovieActors.Attach(item);
            }
            foreach (var item in movie.MovieTheaters)
            {
                _dbContext.MovieTheaters.Attach(item);
            }
           await _dbContext.Movies.AddAsync(movie);
        }
    }
}

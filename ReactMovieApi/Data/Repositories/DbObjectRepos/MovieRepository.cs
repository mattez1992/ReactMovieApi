using Microsoft.EntityFrameworkCore;
using ReactMovieApi.DTOs.MovieDtos;
using ReactMovieApi.Extensions;
using ReactMovieApi.Helpers;
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
        public async Task<List<Movie>> FilterMovies(HttpContext httpContext, FilterMoviesDto filterMoviesDto)
        {
            var query = _dbContext.Movies.AsQueryable();
            await httpContext.InsertPageCountInPaginationHeader(query);

            if (!string.IsNullOrWhiteSpace(filterMoviesDto.Title) && filterMoviesDto.Title != "")
            {
                query = query.Where(x => x.Title.Contains(filterMoviesDto.Title));
            }

            if ((bool)filterMoviesDto.InTheaters)
            {
                query = query.Where(x => x.inTheaters);
            }

            if ((bool)filterMoviesDto.UpcomingReleases)
            {
                var today = DateTime.Today;
                query = query.Where(x => x.ReleaseDate > today);
            }

            if (filterMoviesDto.GenreId != 0)
            {
                query = query
                    .Where(x => x.MoviesGenres.Select(y => y.GenreId)
                    .Contains(filterMoviesDto.GenreId));
            }
           
            var movies = await query.Paginate(filterMoviesDto.PaginationDto).ToListAsync();
            return movies;
        }
        public void RemoveRelatedActors(Movie movie)
        {
            _dbContext.MovieActors.RemoveRange(movie.MoviesActors);
        }
        public void RemoveRelatedTheater(Movie movie)
        {
             _dbContext.MovieMovieTheaters.RemoveRange(movie.MovieTheatersMovies);
        }
        public void RemoveRelatedGenres(Movie movie)
        {
            _dbContext.GenreMovies.RemoveRange(movie.MoviesGenres);
        }
        public async Task InsertMovie(Movie movie)
        {
            foreach (var item in movie.MoviesGenres)
            {
                _dbContext.GenreMovies.Attach(item);
            }
            foreach (var item in movie.MoviesActors)
            {
                _dbContext.MovieActors.Attach(item);
            }
            foreach (var item in movie.MovieTheatersMovies)
            {
                _dbContext.MovieMovieTheaters.Attach(item);
            }
           await _dbContext.Movies.AddAsync(movie);
        }
    }
}

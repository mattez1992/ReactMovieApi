using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMovieApi.Data.Repositories;
using ReactMovieApi.DTOs;
using ReactMovieApi.DTOs.MovieActorsDTOs;
using ReactMovieApi.DTOs.MovieDtos;
using ReactMovieApi.DTOs.MovieTheaterDTOs;
using ReactMovieApi.Models;
using ReactMovieApi.Services;

namespace ReactMovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IFileStorageService _fileStorageService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly string _movieFileContainerName = "movies";

        public MovieController(IUnitOfWork repository, IMapper mapper, ILogger<MovieController> logger, IFileStorageService fileStorageService, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _fileStorageService = fileStorageService;
            _userManager = userManager;
        }

        [HttpGet()]
        [AllowAnonymous]
        public async Task<IActionResult> Get([FromQuery] PaginationDto pageRequest)
        {
            var top = 6;
            var today = DateTime.Today;

            var upcomingReleases = (await _repository.Movies.GettAllEntities(expression: x => x.ReleaseDate > today, orderBy: x => x.OrderBy(y => y.ReleaseDate))).Take(top).ToList();
            var inTheaters = (await _repository.Movies.GettAllEntities(expression: x => x.inTheaters, orderBy: x => x.OrderBy(y => y.ReleaseDate))).Take(top).ToList();

            var indexPageDto = new MovieIndexPageDto() { InTheaters = _mapper.Map<List<MovieReadDto>>(inTheaters), UpComingReleases = _mapper.Map<List<MovieReadDto>>(upcomingReleases) };
            return Ok(indexPageDto);
        }

        [HttpGet("{id:int}")]
        [AllowAnonymous]
        public async Task<ActionResult<MovieReadDto>> Get(int id)
        {
            _logger.LogInformation($"Getting movie with id {id}");
            var movie = await _repository.Movies.GetEntity(x => x.Id == id, includes: x => x.Include(x => x.MovieTheatersMovies).ThenInclude(x => x.MovieTheater).Include(x => x.MoviesGenres).ThenInclude(x => x.Genre).Include(x => x.MoviesActors).ThenInclude(x => x.Actor));
            if (movie == null)
            {
                return NotFound();
            }

            var averageRating = 0.0;
            var userRating = 0;

            if(await _repository.Ratings.EntitesExists(x => x.MovieId == id))
            {
                averageRating = (await _repository.Ratings.GettAllEntities(expression:x => x.MovieId == id)).Average(x => x.Rate);
                if (HttpContext.User.Identity.IsAuthenticated)
                {
                    var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
                    var user = await _userManager.FindByEmailAsync(email);
                    var userId = user.Id;
                    var existingUserRating = await _repository.Ratings.GetEntity(x => x.MovieId == id && x.UserId == userId);
                    if(existingUserRating != null)
                    {
                        userRating = existingUserRating.Rate;
                    }
                }
            }
            var movieDto = _mapper.Map<MovieReadDto>(movie);
            movieDto.AverageRating = averageRating;
            movieDto.UserRate = userRating;
            movieDto.MoviesActors = movieDto.MoviesActors.OrderBy(x => x.Order).ToList();
            return movieDto;
        }
        [HttpGet("filter")]
        [AllowAnonymous]
        public async Task<ActionResult<List<MovieReadDto>>> Filter([FromQuery] FilterMoviesDto filterMoviesDto)
        {
            var movies = await _repository.Movies.FilterMovies(HttpContext,filterMoviesDto);
            return _mapper.Map<List<MovieReadDto>>(movies);
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MovieCreateDto createDto)
        {
            var newmovie = _mapper.Map<Movie>(createDto);
            if (createDto.Poster != null)
            {
                newmovie.Poster = await _fileStorageService.SaveFile(_movieFileContainerName, createDto.Poster);
            }
            AnnotateActorsOrder(newmovie);
            await _repository.Movies.InsertMovie(newmovie);
            await _repository.SaveChanges();
            return Ok();
        }
        private void AnnotateActorsOrder(Movie movie)
        {
            if (movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }
        [HttpGet("getput/{id:int}")]
        public async Task<IActionResult> GetEditDto(int id)
        {
            var movieActionResult = await Get(id);
            if (movieActionResult == null && movieActionResult.Result != null)
            {
                return NotFound();
            }
            var movie = movieActionResult.Value;
            var selectedGenreIds = movie.Genres.Select(x => x.Id).ToList();
            var nonSelectedGenres = await _repository.Genres.GettAllEntities(expression: x => !selectedGenreIds.Contains(x.Id));

            var selectedMovieTheatersIds = movie.MovieTheaters.Select(x => x.Id).ToList();
            var nonSelectedMovieTheaters = await _repository.MovieTheaters.GettAllEntities(expression: x => !selectedMovieTheatersIds.Contains(x.Id));

            var nonSelectedGenredDtos = _mapper.Map<List<GenreReadDto>>(nonSelectedGenres);
            var nonSelectedMovieTheatersDtos = _mapper.Map<List<MovieTheaterReadDto>>(nonSelectedMovieTheaters);

            var response = new MovieEditPageDto();
            response.Movie = movie;
            response.SelectedGenres = movie.Genres.ToList();
            response.NonSelectedGenres = nonSelectedGenredDtos;
            response.SelectedMovieTheaters = movie.MovieTheaters.ToList();
            response.NonSelectedMovieTheaters = nonSelectedMovieTheatersDtos;
            response.Actors = movie.MoviesActors.ToList();
            return Ok(response);
        }
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] MovieCreateDto creationDto)
        {
            var existingmovie = await _repository.Movies.GetEntity(x => x.Id == id, includes: x => x.Include(x => x.MoviesGenres).Include(x => x.MovieTheatersMovies).Include(x => x.MoviesActors));
                    
            if (existingmovie == null)
            {
                return NotFound();
            }
            if(creationDto.Actors != null && creationDto.Actors.Count > 0 && existingmovie.MoviesActors != null)
            {
                _repository.Movies.RemoveRelatedActors(existingmovie);
            }
            if(creationDto.MovieTheatersIds != null && creationDto.MovieTheatersIds.Count > 0 && existingmovie.MovieTheatersMovies != null)
            {
                _repository.Movies.RemoveRelatedTheater(existingmovie);
            }
            if(creationDto.GenresIds != null && creationDto.GenresIds.Count > 0 && existingmovie.MoviesGenres != null)
            {
                _repository.Movies.RemoveRelatedGenres(existingmovie);
            }
            existingmovie = _mapper.Map(creationDto, existingmovie);

            if (creationDto.Poster != null)
            {
                existingmovie.Poster = await _fileStorageService.EditFile(_movieFileContainerName, creationDto.Poster, existingmovie.Poster);
            }

            AnnotateActorsOrder(existingmovie);
            _repository.Movies.Update(existingmovie);
            await _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var movieToDelete = await _repository.Movies.GetEntity(x => x.Id == id);
            if (movieToDelete == null)
            {
                return NotFound();
            }

            await _repository.Movies.Delete(id);
            await _fileStorageService.DeleteFile(movieToDelete.Poster, _movieFileContainerName);
            await _repository.SaveChanges();
            return NoContent();
        }
    }
}

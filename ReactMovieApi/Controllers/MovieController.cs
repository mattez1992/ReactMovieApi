using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ReactMovieApi.Data.Repositories;
using ReactMovieApi.DTOs;
using ReactMovieApi.DTOs.MovieActorsDTOs;
using ReactMovieApi.DTOs.MovieDtos;
using ReactMovieApi.Models;
using ReactMovieApi.Services;

namespace ReactMovieApi.Controllers
{
    [Route("api/movies")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;
        private readonly IFileStorageService _fileStorageService;
        private readonly string _movieFileContainerName = "movies";

        public MovieController(IUnitOfWork repository, IMapper mapper, ILogger<MovieController> logger, IFileStorageService fileStorageService)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _fileStorageService = fileStorageService;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] PaginationDto pageRequest)
        {
            _logger.LogInformation("Getting all movies");
            var movies = await _repository.Movies.GetPages(HttpContext, pageRequest, includes: x => x.Include(x => x.MovieTheaters).Include(x => x.Genres).Include(x => x.MoviesActors).ThenInclude(x => x.Actor));
            return Ok(_mapper.Map<IEnumerable<MovieReadDto>>(movies));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting movie with id {id}");
            var movie = await _repository.Movies.GetEntity(x => x.Id == id, includes: x => x.Include(x => x.MovieTheaters).Include(x => x.Genres).Include(x => x.MoviesActors).ThenInclude(x => x.Actor));
            if (movie == null)
            {
                return NotFound();
            }
            var movieDto = _mapper.Map<MovieReadDto>(movie);
            movieDto.MoviesActors = movieDto.MoviesActors.OrderBy(x => x.Order).ToList();
            return Ok(movieDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromForm] MovieCreateDto createDto)
        {
            //[FromForm] MovieCreateDto createDto
            //var Actors = new MovieActorsCreationDto { Id = 1, Character = "Spider man" };
            //var genresId = new List<int> { 1};
            //var movieTheaterIds = new List<int> { 1};
            //var createDto = new MovieCreateDto 
            //{ 
            //    Title = "Spider Man: Far from Home", 
            //    Actors = new List<MovieActorsCreationDto> { Actors }, 
            //    dateTime = DateTime.Now, GenresIds = genresId, 
            //    inTheaters = true, MovieTheatersIds = movieTheaterIds,
            //    Summary = "Spider man does things", Trailer = "No trailer" };
            var newmovie = _mapper.Map<Movie>(createDto);
            if(createDto.Poster != null)
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
            if(movie.MoviesActors != null)
            {
                for (int i = 0; i < movie.MoviesActors.Count; i++)
                {
                    movie.MoviesActors[i].Order = i;
                }
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromForm] MovieReadDto creationDto)
        {
            var existingmovie = await _repository.Movies.GetEntity(x => x.Id == id);
            if (existingmovie == null)
            {
                return NotFound();
            }
            existingmovie = _mapper.Map<Movie>(creationDto);
            existingmovie.Id = id;


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
            await _repository.SaveChanges();
            return NoContent();
        }
    }
}

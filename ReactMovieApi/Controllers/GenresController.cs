using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactMovieApi.Data.Repositories;
using ReactMovieApi.DTOs;
using ReactMovieApi.Models;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesApi.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public GenresController(IUnitOfWork repository,IMapper mapper, ILogger<GenresController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }
        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting all genres");
            return Ok(await _repository.Genres.GettAllEntities());
        }

        // GET api/<GenresController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting genre with id {id}");
            var genre = await _repository.Genres.GetEntity(x => x.Id == id);
            if (genre == null)
            {
                _logger.LogWarning($"Could not find genre with id {id}");
                return NotFound();
            }
            return Ok(genre);
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] GenreCreationDto newGenreDto)
        {
            _logger.LogInformation("Createing a genre");
            var newGenre = _mapper.Map<Genre>(newGenreDto);
            await _repository.Genres.Insert(newGenre);
            await _repository.SaveChanges();
            return Ok();
        }

        // PUT api/<GenresController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] string value)
        {
            return NoContent();
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            return NoContent();
        }
    }
}

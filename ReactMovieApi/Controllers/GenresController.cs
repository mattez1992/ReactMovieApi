using Microsoft.AspNetCore.Mvc;
using ReactMovieApi.Models;
using ReactMovieApi.Services;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MoviesApi.Controllers
{
    [Route("api/genre")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly IRepository _repository;
        private readonly ILogger _logger;

        public GenresController(IRepository repository, ILogger<GenresController> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        // GET: api/<GenresController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting all genres");
            return Ok(await _repository.GetAllGenres());
        }

        // GET api/<GenresController>/5
        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting genre with id {id}");
            var genre = await _repository.Get(id);
            if (genre == null)
            {
                _logger.LogWarning($"Could not find genre with id {id}");
                return NotFound();
            }
            return Ok(genre);
        }

        // POST api/<GenresController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Genre genre)
        {
            _logger.LogInformation("Createing a genre");
            _repository.AddGenre(genre);
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

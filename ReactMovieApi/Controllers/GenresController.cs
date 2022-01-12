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
        public async Task<IActionResult> Get([FromQuery] PageRequest pageRequest)
        {
            _logger.LogInformation("Getting all genres");
            return Ok(_mapper.Map<IEnumerable<GenreReadDto>>(await _repository.Genres.GettAllEntities(pageRequest)));
        }
        [HttpGet("pages")]
        public async Task<IActionResult> GetPages([FromQuery] PaginationDto pageRequest)
        {
            _logger.LogInformation("Getting all genres");
            var genres = await _repository.Genres.GetPages(HttpContext, pageRequest);
            return Ok(_mapper.Map<IEnumerable<GenreReadDto>>(genres));
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
            var genreReadDto = _mapper.Map<GenreReadDto>(genre);
            return Ok(genreReadDto);
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
        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] GenreCreationDto creationDto)
        {
            var existingGenre = await _repository.Genres.GetEntity(x => x.Id == id);
            if(existingGenre == null)
            {
                return NotFound();
            }
            existingGenre.Name = _mapper.Map<Genre>(creationDto).Name;
            _repository.Genres.Update(existingGenre);
            await _repository.SaveChanges();
            return NoContent();
        }

        // DELETE api/<GenresController>/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genreToDelete = await _repository.Genres.GetEntity(x => x.Id == id);
            if(genreToDelete == null)
            {
                return NotFound();
            }
            await _repository.Genres.Delete(id);
            await _repository.SaveChanges();
            return NoContent();
        }
    }
}

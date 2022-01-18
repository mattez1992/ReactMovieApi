using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactMovieApi.Data.Repositories;
using ReactMovieApi.DTOs;
using ReactMovieApi.DTOs.MovieTheaterDTOs;
using ReactMovieApi.Models;
using ReactMovieApi.Services;

namespace ReactMovieApi.Controllers
{
    [Route("api/movietheaters")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
    public class MovieTheaterController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public MovieTheaterController(IUnitOfWork repository, IMapper mapper,  ILogger<MovieTheaterController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] PaginationDto pageRequest)
        {
            _logger.LogInformation("Getting all movietheaters");
            var movietheaters = await _repository.MovieTheaters.GetPages(HttpContext, pageRequest);
            return Ok(_mapper.Map<IEnumerable<MovieTheaterReadDto>>(movietheaters));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting movietheater with id {id}");
            var movietheater = await _repository.MovieTheaters.GetEntity(x => x.Id == id);
            if (movietheater == null)
            {
                _logger.LogWarning($"Could not find movietheate with id {id}");
                return NotFound();
            }
            var movietheaterDto = _mapper.Map<MovieTheaterReadDto>(movietheater);
            return Ok(movietheaterDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] MovieTheaterCreateDto createDto)
        {
            var newTheater = _mapper.Map<MovieTheater>(createDto);
   
            await _repository.MovieTheaters.Insert(newTheater);
            await _repository.SaveChanges();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] MovieTheaterCreateDto creationDto)
        {
            var existingTheater = await _repository.MovieTheaters.GetEntity(x => x.Id == id);
            if (existingTheater == null)
            {
                return NotFound();
            }
            existingTheater = _mapper.Map<MovieTheater>(creationDto);
            existingTheater.Id = id;

           
            _repository.MovieTheaters.Update(existingTheater);
            await _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var theaterToDelete = await _repository.MovieTheaters.GetEntity(x => x.Id == id);
            if (theaterToDelete == null)
            {
                return NotFound();
            }

            await _repository.MovieTheaters.Delete(id);
            await _repository.SaveChanges();
            return NoContent();
        }
    }
}

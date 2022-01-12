﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ReactMovieApi.Data.Repositories;
using ReactMovieApi.DTOs;
using ReactMovieApi.DTOs.ActorDTOs;
using ReactMovieApi.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ReactMovieApi.Controllers
{
    [Route("api/actor")]
    [ApiController]
    public class ActorController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public ActorController(IUnitOfWork repository, IMapper mapper, ILogger<ActorController> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Get([FromQuery] PaginationDto pageRequest)
        {
            _logger.LogInformation("Getting all actors");
            var actors = await _repository.Actors.GetPages(HttpContext, pageRequest);
            return Ok(_mapper.Map<IEnumerable<ActorReadDto>>(actors));
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            _logger.LogInformation($"Getting actor with id {id}");
            var actor = await _repository.Actors.GetEntity(x => x.Id == id);
            if (actor == null)
            {
                _logger.LogWarning($"Could not find actor with id {id}");
                return NotFound();
            }
            var actorReadDto = _mapper.Map<ActorReadDto>(actor);
            return Ok(actorReadDto);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ActorCreateDto actorDto)
        {
            _logger.LogInformation("Createing a actor");
            var newActor = _mapper.Map<Actor>(actorDto);
            await _repository.Actors.Insert(newActor);
            await _repository.SaveChanges();
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Put(int id, [FromBody] ActorCreateDto creationDto)
        {
            var existingActor = await _repository.Actors.GetEntity(x => x.Id == id);
            if (existingActor == null)
            {
                return NotFound();
            }
            existingActor = _mapper.Map<Actor>(creationDto);
            existingActor.Id = id;
            _repository.Actors.Update(existingActor);
            await _repository.SaveChanges();
            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var genreToDelete = await _repository.Actors.GetEntity(x => x.Id == id);
            if (genreToDelete == null)
            {
                return NotFound();
            }
            await _repository.Actors.Delete(id);
            await _repository.SaveChanges();
            return NoContent();
        }
    }
}

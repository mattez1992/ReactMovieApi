using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ReactMovieApi.Data.Repositories;
using ReactMovieApi.DTOs.RatingDTOs;
using ReactMovieApi.Models;

namespace ReactMovieApi.Controllers
{
    [Route("api/ratings")]
    [ApiController]
    public class RaitingsController : ControllerBase
    {
        private readonly IUnitOfWork _repository;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;

        public RaitingsController(IUnitOfWork repository, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _repository = repository;
            _mapper = mapper;
            _userManager = userManager;
        }
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] RatingCreateDTO ratingDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == "email").Value;
            var user = await _userManager.FindByEmailAsync(email);
            var userId = user.Id;

            var currentRate = await _repository.Ratings.GetEntity(expression: x => x.MovieId == ratingDTO.MovieId && x.UserId == userId);

            if (currentRate == null)
            {
                var rating = new Rating();
                rating.MovieId = ratingDTO.MovieId;
                rating.Rate = ratingDTO.Rating;
                rating.UserId = userId;
                await _repository.Ratings.Insert(rating);
            }
            else
            {
                currentRate.Rate = ratingDTO.Rating;
                _repository.Ratings.Update(currentRate);
            }

            await _repository.SaveChanges();
            return NoContent();
        }
    }
}

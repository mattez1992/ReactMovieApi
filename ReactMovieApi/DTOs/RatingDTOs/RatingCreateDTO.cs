using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.DTOs.RatingDTOs
{
    public class RatingCreateDTO
    {
        [Range(1, 5)]
        public int Rating { get; set; }
        public int MovieId { get; set; }
    }
}

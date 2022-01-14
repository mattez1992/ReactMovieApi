using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.DTOs.MovieTheaterDTOs
{
    public class MovieTheaterCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [Range(-90, 90)]
        public double Latitude { get; set; }
        [Range(-180, 180)]
        public double Longitude { get; set; }
    }
}

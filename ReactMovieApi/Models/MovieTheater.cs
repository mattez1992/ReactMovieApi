using NetTopologySuite.Geometries;
using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.Models
{
    public class MovieTheater : BaseDbObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public Point Location { get; set; }
        public virtual IList<Movie> Movies { get; set;}
    }
}

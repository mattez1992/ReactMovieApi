using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ReactMovieApi.Models
{
    public class Rating : BaseDbObject
    {     
        [Range(1, 5)]
        public int Rate { get; set; }
        [ForeignKey(nameof(Movie))]
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        [ForeignKey(nameof(User))]
        public string UserId { get; set; }
        public IdentityUser User { get; set; }
    }
}

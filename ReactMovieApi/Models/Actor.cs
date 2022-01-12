using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.Models
{
    public class Actor : BaseDbObject
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Biography { get; set; }
        public string Picture { get; set; }
    }
}

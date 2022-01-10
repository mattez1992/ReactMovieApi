using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.Models
{
    public class BaseDbObject
    {
        [Key]
        public int Id { get; set; }
    }
}

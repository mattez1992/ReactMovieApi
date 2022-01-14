using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.Models
{
    public abstract class BaseDbObject
    {
        [Key]
        public int Id { get; set; }
    }
}

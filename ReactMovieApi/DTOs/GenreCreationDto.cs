using ReactMovieApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.DTOs
{
    public class GenreCreationDto
    {
        [Required(ErrorMessage = "The field with the name {0} is required.")]
        [StringLength(50)]
        [FirstLetterUpperCase]
        public string Name { get; set; }
    }
}

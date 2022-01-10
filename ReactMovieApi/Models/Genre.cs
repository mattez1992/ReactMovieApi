using ReactMovieApi.Validations;
using System.ComponentModel.DataAnnotations;

namespace ReactMovieApi.Models
{
    public class Genre : IValidatableObject
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "The field with the name {0} is required.")]
        [StringLength(50)]
        [FirstLetterUpperCase]
        public string Name { get; set; }

        // testing to using IValidableObject interface to validate firsletter upper case.
        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!string.IsNullOrWhiteSpace(Name))
            {
                var firstLetter = Name.ToString()[0].ToString();
                if (firstLetter != firstLetter.ToUpper())
                {
                    yield return new ValidationResult("First letter should be uppercase", new string[] { nameof(Name) });
                }
            }
        }
    }
}

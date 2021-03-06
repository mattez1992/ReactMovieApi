namespace ReactMovieApi.Models
{
    public class MoviesActors
    {
        public int ActorId { get; set; }
        public Actor Actor { get; set; }
        public int MovieId { get; set; }
        public Movie Movie { get; set; }
        public string MovieCharacter { get; set; }
        public int Order { get; set; }          
    }
}

namespace ReactMovieApi.DTOs.MovieDtos
{
    public class MovieIndexPageDto
    {
        public List<MovieReadDto> InTheaters { get; set; }
        public List<MovieReadDto> UpComingReleases { get; set; }
    }
}

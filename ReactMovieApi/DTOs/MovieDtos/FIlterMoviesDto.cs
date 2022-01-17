namespace ReactMovieApi.DTOs.MovieDtos
{
    public class FilterMoviesDto
    {
        public int Page { get; set; } = 1;
        public int RecordsPerPage { get; set; } = 10;
        public PaginationDto PaginationDto
        {
            get { return new PaginationDto() { Page = Page, RecordsperPage = RecordsPerPage }; }
        }
        public string? Title { get; set; } = string.Empty;
        public int GenreId { get; set; } = 0;
        public bool? InTheaters { get; set; } = false;
        public bool? UpcomingReleases { get; set; } = false;
    }
}

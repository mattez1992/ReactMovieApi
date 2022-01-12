using ReactMovieApi.DTOs;

namespace ReactMovieApi.Extensions
{
    public static class IQueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDto paginationDto)
        {
            return queryable.Skip((paginationDto.Page - 1) * paginationDto.RecordsperPage)
                .Take(paginationDto.RecordsperPage);
        }
    }
}

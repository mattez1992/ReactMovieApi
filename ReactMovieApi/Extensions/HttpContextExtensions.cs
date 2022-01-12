using Microsoft.EntityFrameworkCore;

namespace ReactMovieApi.Helpers
{
    public static class HttpContextExtensions
    {
        public async static Task InsertPageCountInPaginationHeader<T>(this HttpContext context, IQueryable<T> query)
        {
            if (context == null) { throw new ArgumentNullException(nameof(context)); }

            double count = await query.CountAsync();
            context.Response.Headers.Add("totalAmountOfrecords", count.ToString());
        }
    }
}

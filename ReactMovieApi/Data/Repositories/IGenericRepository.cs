using Microsoft.EntityFrameworkCore.Query;
using ReactMovieApi.Models;
using System.Linq.Expressions;

namespace ReactMovieApi.Data.Repositories
{
    public interface IGenericRepository<T> where T : BaseDbObject
    {
        Task<IList<T>> GettAllEntities(
            Expression<Func<T, bool>> expression = null,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null
            );
        Task<T?> GetEntity(Expression<Func<T, bool>> expression,
            Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null);
        Task Insert(T entity);
        Task InserRange(IEnumerable<T> entities);
        Task Delete(int id);
        void DeleteRange(IEnumerable<T> entities);
        void Update(T entity);
    }
}

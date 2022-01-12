using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using ReactMovieApi.DTOs;
using ReactMovieApi.Extensions;
using ReactMovieApi.Helpers;
using ReactMovieApi.Models;
using System.Linq;
using System.Linq.Expressions;
using X.PagedList;

namespace ReactMovieApi.Data.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : BaseDbObject
    {
        protected readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<T?> GetEntity(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (includes != null)
            {
                query = includes(query);
            }
            return await query.AsNoTracking().FirstOrDefaultAsync(expression);
            
        }

        public async Task<IPagedList<T>> GettAllEntities(PageRequest pageRequest, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (expression != null)
            {
                query.Where(expression);
            }
            if (includes != null)
            {
                query = includes(query);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            return await query.AsNoTracking().ToPagedListAsync(pageRequest.Page, pageRequest.RecordsperPage);
        }

        // other pagination solution 
        public async Task<List<T>> GetPages(HttpContext httpContext, PaginationDto paginationDto, Expression<Func<T, bool>> expression = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, Func<IQueryable<T>, IIncludableQueryable<T, object>> includes = null)
        {
            var query = _dbContext.Set<T>().AsQueryable();
            await httpContext.InsertPageCountInPaginationHeader(query);
            if (expression != null)
            {
                query.Where(expression);
            }
            if (includes != null)
            {
                query = includes(query);
            }
            if (orderBy != null)
            {
                query = orderBy(query);
            }
            var pages = await query.Paginate(paginationDto).ToListAsync();
            return pages;
        }

        public async Task Insert(T entity)
        {
            if (entity != null)
            {
                await _dbContext.Set<T>().AddAsync(entity);
            }
        }
        public async Task InserRange(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                await _dbContext.Set<T>().AddRangeAsync(entities);
            }
        }



        public void Update(T entity)
        {
            _dbContext.Set<T>().Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public async Task Delete(int id)
        {
            var recordToDelete = await _dbContext.Set<T>().FindAsync(id);
            if (recordToDelete != null)
            {
                _dbContext.Set<T>().Remove(recordToDelete);
            }
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            if (entities != null)
            {
                _dbContext.Set<T>().RemoveRange(entities);
            }
        }
    }
}

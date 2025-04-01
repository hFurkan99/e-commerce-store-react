using App.Application.Helpers;
using App.Domain.Entities.Common;
using System.Linq.Expressions;

namespace App.Application.Contracts.Persistence;

public interface IGenericRepository<T, TKey> where T : class where TKey : struct
{
    Task<List<T>> GetAllAsync();
    Task<List<T>> WhereAsync(Expression<Func<T, bool>> predicate);
    Task<T?> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(Expression<Func<T, bool>> predicate);
    Task<bool> AnyAsync(TKey id);
    ValueTask<T?> GetByIdAsync(TKey id);
    Task<PaginatedResult<T>> GetPagedAsync(int pageNumber, int pageSize);
    ValueTask AddAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task<PagedList<T>> ToPagedList(IQueryable<T> query, int pageNumber, int pageSize);
}

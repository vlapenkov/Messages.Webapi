using FileStore.Domain;
using System.Linq.Expressions;

namespace FileStore.Interfaces.Interfaces
{
    /// <summary>
    /// Интерфейс generic репозитория
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T, bool>> predicate);

        Task<T> GetByIdAsync(long id);

        Task<IEnumerable<T>> GetRangeByIdsAsync(List<long> ids);

        Task AddAsync(T entity);

        Task UpdateAsync(T entity);

        Task DeleteAsync(T entity);

        Task SaveChangesAsync();

    }
}

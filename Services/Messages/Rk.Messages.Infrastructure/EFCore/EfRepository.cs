using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Rk.Messages.Domain.Entities;
using Rk.Messages.Interfaces.Interfaces.DAL;

namespace Rk.Messages.Infrastructure.EFCore
{
    public class EfRepository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly AppDbContext _dataContext;

        public EfRepository(AppDbContext dataContext)
        {
            _dataContext = dataContext;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dataContext.Set<T>().Where(predicate).AsNoTracking();
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            var entity = await _dataContext.Set<T>().FirstOrDefaultAsync(x => x.Id == id);

            return entity;
        }

        public async Task<IEnumerable<T>> GetRangeByIdsAsync(List<long> ids)
        {
            var entities = await _dataContext.Set<T>().Where(x => ids.Contains(x.Id)).ToListAsync();
            return entities;
        }


        public async Task AddAsync(T entity)
        {
            await _dataContext.Set<T>().AddAsync(entity);
            await _dataContext.SaveChangesAsync();

        }

        public async Task UpdateAsync(T entity)
        {
            await _dataContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(T entity)
        {
            _dataContext.Set<T>().Remove(entity);
        }


    }

}

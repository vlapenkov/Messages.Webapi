using Messages.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Messages.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {
        IQueryable<T> GetAll(Expression<Func<T,bool>> predicate);
        T GetById(long id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);       
    }
}

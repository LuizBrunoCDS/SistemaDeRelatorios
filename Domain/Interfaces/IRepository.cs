using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IEnumerable<T> SelectAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "");
        T SelectByParam(Expression<Func<T, bool>> filter = null, string includeProperties = "");
        void Insert(T entity);
        void Update(T oldEntity, T newEntity);
        void Delete(T entity);
    }
}

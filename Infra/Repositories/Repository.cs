using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Domain.Interfaces;
using Infra.Context;

namespace Infra.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        internal RelatoriosContext context;
        internal DbSet<T> dbSet;

        public Repository(RelatoriosContext _context)
        {
            context = _context;
            dbSet = context.Set<T>();
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void Insert(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> SelectAll(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            if (includeProperties != "")
                foreach (var include in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                    query = query.Include(include);
            if (filter != null)
                query = query.Where(filter);
            if (orderBy != null)
                return orderBy(query).ToList();
            else
                return query.ToList();
        }

        public T SelectByParam(Expression<Func<T, bool>> filter = null, string includeProperties = "")
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                query = query.Include(include);
            return query.FirstOrDefault(filter);
        }

        public void Update(T oldEntity, T newEntity)
        {
            context.Entry(oldEntity).CurrentValues.SetValues(newEntity);
        }
    }
}

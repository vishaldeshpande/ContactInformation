using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Http;

namespace ContactInformation.DataService
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        DbSet<T> dbSet;
        private CustomerContactDBContext dbContext;

        public GenericRepository(CustomerContactDBContext _dbContext)
        {
            dbContext = _dbContext;
            dbSet = dbContext.Set<T>();
            dbContext.Configuration.ProxyCreationEnabled = false;
            GlobalConfiguration.Configuration.Formatters.JsonFormatter.SerializerSettings.Re‌​ferenceLoopHandling = ReferenceLoopHandling.Ignore;
        }

        /// <summary>
        /// Get Single Object
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public T Get(Int32 id)
        {
            return dbSet.Find(id);
        }

        /// <summary>
        /// Get List of all objects
        /// </summary>
        /// <returns></returns>
        public List<T> GetAll()
        {
            return dbSet.ToList();
        }

        /// <summary>
        /// Filter - Search by any parameter of object
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public IEnumerable<T> Filter(Expression<Func<T, bool>> filter)
        {
            var result = Enumerable.Empty<T>();
            result = dbSet.Where(filter).ToList();

            return result;
        }

        /// <summary>
        /// Post - Used for Adding
        /// </summary>
        /// <param name="t">Class object which we like to add</param>
        /// <returns></returns>
        public int Post(T t)
        {
            dbSet.Add(t);
            dbContext.Entry(t).State = EntityState.Added;
            return dbContext.SaveChanges();
        }

        /// <summary>
        /// Put - Used for Update
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Put(T t, int id)
        {
            T entity = dbSet.Find(id);
            
            dbContext.Entry(entity).CurrentValues.SetValues(t);

            return dbContext.SaveChanges();
        }


        /// <summary>
        /// Remove - Used for Removal
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public int Remove(T t)
        {
            dbSet.Attach(t);
            dbContext.Entry(t).State = EntityState.Deleted;
            return dbContext.SaveChanges();
        }

        public virtual T GetSingle(Func<T, bool> where, params Expression<Func<T, object>>[] navigationProperties)
        {
            T item = null;

            IQueryable<T> dbQuery = dbContext.Set<T>();

            //Apply eager loading
            foreach (Expression<Func<T, object>> navigationProperty in navigationProperties)
                dbQuery = dbQuery.Include<T, object>(navigationProperty);

            item = dbQuery
                .AsNoTracking() //Don't track any changes for the selected item
                .FirstOrDefault(where); //Apply where clause
            return item;
        }

        public List<T> Get(Expression<Func<T, bool>> filter = null,
         //   Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;

            foreach (Expression<Func<T, object>> include in includes)
                query = query.Include(include);

            if (filter != null)
                query = query.AsNoTracking().Where(filter);

            //if (orderBy != null)
            //    query = orderBy(query);

            return query.ToList();
        }
    }
}
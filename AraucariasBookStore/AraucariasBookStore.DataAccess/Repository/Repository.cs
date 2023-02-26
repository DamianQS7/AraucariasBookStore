using AraucariasBookStore.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.DataAccess.Repository
{
    /// <summary>
    /// The purpose of this class is to handle all the operations performed over the database.
    /// Since our database will contain different entities, it is defined as a generic class.
    /// 
    /// Also, since we want to centralize our database interaction here, we will create a db context, so we can
    /// remove the db contexts created in the controlles.
    /// </summary>
    /// <typeparam name="T">Database Entities</typeparam>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;

        // Here we are creating a generic DbSet, so we can use it later to perform the operations in the DB
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            dbSet = _db.Set<T>();
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public IEnumerable<T> GetAll()
        {
            IQueryable<T> query = dbSet.AsQueryable();
            return query.ToList();
        }

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            IQueryable<T> query = dbSet.AsQueryable();

            query = query.Where(filter);

            return query.FirstOrDefault();
        }

        public void Remove(T entity)
        {
            dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }
    }
}

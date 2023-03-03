using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AraucariasBookStore.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // To understand this, imagine that T is our Category class (entity).
        // What are the operations that we have performed over Categories in our controllers?
        // That is what we need to capture here.


        /// <summary>
        /// This method will retrieve the collection representing all the records from a specific entity.
        /// Like when we say _dbContext.Categories.ToList();
        /// </summary>
        /// <returns>We are returning a collection of the specific class or entity</returns>
        IEnumerable<T> GetAll(string? includeProperties = null);

        /// <summary>
        /// It will add the entity to a DbSet. That is why the return datatype is Void.
        /// </summary>
        /// <param name="entity">The entity (class) to be added to the collection</param>
        void Add(T entity);

        /// <summary>
        /// It will remove the entity from the DbSet. That is why the return datatype is Void.
        /// </summary>
        /// <param name="entity">The entity (class) to be removed from the collection</param>
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);

        /// <summary>
        /// This method is trying to emulate the FirstOrDefault method, which accepts a lambda expression as parameter.
        /// The Expression<TDelegate> Class represents a strongly typed lambda expression as a data structure in the form 
        /// of an expression tree.
        /// </summary>
        /// <param name="filter">This lambda expression will act as a filter to find and retrieve a specific record from a collection.</param>
        /// <returns>The return data type is T because it will return a single entity (Category, Book, etc.)</returns>
        T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null);


    }
}

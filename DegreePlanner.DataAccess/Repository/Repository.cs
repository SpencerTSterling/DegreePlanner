using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace DegreePlanner.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet;

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // ex. _db.Terms == dbSet
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entity)
        {
            dbSet.RemoveRange(entity);
        }

        /// <summary>
        /// Retrieves a single entity that matches the specified filter.
        /// </summary>
        /// <param name="filter"> An expression to filter the entities. </param>
        /// <param name="includeProperties"> Comma seperated list of related entities to inclide in the query result. 
        ///                                  Example: "Course,CourseItem"</param>
        /// <returns>The first entity that matches the fulter, or null if no entitiy matches</returns>
        public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null)
        {
            // Start with the DbSet for the entity type T
            IQueryable<T> query = dbSet;
            // Apply the filter expression to the query
            query = query.Where(filter); // WHERE whatever condition is passed (filter)
            // If includeProperties is specified, include the related entities
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var property in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            return query.FirstOrDefault();
        }

        /// <summary>
        /// Retrieves all entities that match the specified filter, if provided.
        /// </summary>
        /// <param name="filter"> An optional expression to filter the entities. If null, all entities are retrieved. </param>
        /// <param name="includeProperties"> A comma-separated list of related entities to include in the query result. </param>
        /// <returns> An enumerable collection of entities that match the filter. </returns>
        public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null)
        {
            // Start with the DbSet for the entity type T.
            IQueryable<T> query = dbSet;
            // If a filter expression is provided, apply it to the query.
            if (filter != null)
            {
                query = query.Where(filter); // filter results of the GetAll 
            }
            // If includeProperties is specified, include the related entities.
            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach(var property in includeProperties
                    .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(property);
                }
            }
            // Execute the query and return the result as a list.
            return query.ToList();
        }
    }
}

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

    // Repository pattern implementation allows for easier data access management 
    // and makes it simpler to scale data sources in the future.


    // This class is the base class for all repositories. 
    // Other reposistories inheriet from this class.

    public class Repository<T> : IRepository<T> where T : class
    {
        // _db is a private field that holds the database context, showcasing encapsulation
        private readonly ApplicationDbContext _db;
        internal DbSet<T> dbSet; // The internal visibility allows the derived classes to access this property

        public Repository(ApplicationDbContext db)
        {
            _db = db;
            this.dbSet = _db.Set<T>();
            // ex. _db.Terms == dbSet
        }

        public void Update(T entity, object id)
        {
            // Find the existing entity by its ID
            var existingEntity = dbSet.Find(id);
            if (existingEntity != null)
            {
                // Update the values of the existing entity
                _db.Entry(existingEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                // Optionally handle the case where the entity does not exist
                throw new Exception("Entity not found.");
            }
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


        // The generic type T allows for different entity types, showcasing polymorphism
        // The methods Get and GetAll work for different entity types like Term, Course, and CourseItem

        /// <summary>
        /// Retrieves a single entity that matches the specified filter.
        /// This method uses polymorphism to work with any class type T.
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
        /// This method uses polymorphism to handle collections of any class type T.
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

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}

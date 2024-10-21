using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.DataAccess.Repository.IRepository
{
    public interface IRepository<T> where T : class
    {
        // T - Term, Course, CourseItem

        void Update(T entity); // Updates an existing entity
        IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter = null, string? includeProperties = null);
        // For link expressions in FirstOrDefault() methods
        T Get(Expression<Func<T, bool>> filter, string? includeProperties = null);
        void Add(T entity); // Adds a new entity
        void Delete(T entity); // Deletes an existing entity
        // Delete a range/multiple objects in a column
        void DeleteRange(IEnumerable<T> entities);
    }
}

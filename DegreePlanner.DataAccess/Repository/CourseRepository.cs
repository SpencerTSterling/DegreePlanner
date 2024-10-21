using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using DegreePlanner.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.DataAccess.Repository
{
    // CourseRepository interirs from Repository, showcasing inheritance
    // This means CourseRepository can use methods defined in the base Repository class. 

    public class CourseRepository : Repository<Course>, ICourseRepository
    {
        // _db is a private field that holds the database context, showcasing encapsulation
        private readonly ApplicationDbContext _db;
        public CourseRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Course obj)
        {
            var existingEntity = _db.Courses.Local.FirstOrDefault(x => x.Id == obj.Id);
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }

            _db.Courses.Update(obj);
        }
    }
}

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

    // CourseItemRepository interirs from Repository, showcasing inheritance
    // This means CourseItemRepository can use methods defined in the base Repository class. 

    public class CourseItemRepository : Repository<CourseItem>, ICourseItemRepository
    {
        // _db is a private field that holds the database context, showcasing encapsulation
        private readonly ApplicationDbContext _db;

        public CourseItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CourseItem item)
        {
            var existingEntity = _db.CourseItems.Local.FirstOrDefault(x => x.Id == item.Id);
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            
            _db.CourseItems.Update(item);
        }
    }
}

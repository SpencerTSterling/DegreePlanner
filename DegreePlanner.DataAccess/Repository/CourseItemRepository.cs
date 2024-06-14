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
    public class CourseItemRepository : Repository<CourseItem>, ICourseItemRepository
    {
        private readonly ApplicationDbContext _db;

        public CourseItemRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(CourseItem item)
        {
            _db.CourseItems.Update(item);
        }
    }
}

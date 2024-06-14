using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly ApplicationDbContext _db;
        public ITermRepository Term{ get; private set; }
        public ICourseRepository Course { get; private set; }
        public ICourseItemRepository CourseItem { get; private set; }  
        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            // Repositories:
            Term = new TermRepository(_db);
            Course = new CourseRepository(_db);
            CourseItem = new CourseItemRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

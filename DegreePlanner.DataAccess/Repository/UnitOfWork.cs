using capstone.DegreePlanner.DataAccess.Data;
using DegreePlanner.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.DataAccess.Repository
{
    // Unit of Work pattern ensures efficient management of database transactions
    // and promotes scalability by managing multiple repositories in a single context.

    public class UnitOfWork : IUnitOfWork
    {
        // Prpperties for existing repositories

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

            // New repositories can be added here:
            // To add a new entity (e.g., Instructor), do this:
            // 1. Define a new repository interface (e.g., IInstructorRepository) in the IRepository folder
            // 2. Create the repository class (e.g., InstructorRepository) in the Repository folder, and implement the interface
            // 3. Instantiate the new repository here (e.g., Instructor = new InstructorRepository(_db));
            // 4. Update the database 

        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

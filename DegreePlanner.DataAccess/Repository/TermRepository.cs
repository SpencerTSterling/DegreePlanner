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

    // TermRepository interirs from the base class Repository, showcasing inheritance
    // This means TermRepository can use methods defined in the base Repository class. 
    public class TermRepository : Repository<Term>, ITermRepository
    {
        // _db is a private field that holds the database context, showcasing encapsulation
        private readonly ApplicationDbContext _db;
        public TermRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
        public void Update(Term obj)
        {
            var existingEntity = _db.Terms.Local.FirstOrDefault(x => x.Id == obj.Id);
            if (existingEntity != null)
            {
                _db.Entry(existingEntity).State = Microsoft.EntityFrameworkCore.EntityState.Detached;
            }
            _db.Terms.Update(obj);
        }
    }
}

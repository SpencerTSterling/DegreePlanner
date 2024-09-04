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
    public class TermRepository : Repository<Term>, ITermRepository
    {
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

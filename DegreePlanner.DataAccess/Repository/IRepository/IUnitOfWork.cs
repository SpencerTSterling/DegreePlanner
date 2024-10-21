using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DegreePlanner.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork
    {
        // New repositories can be added

        //The repositories:
        ITermRepository Term { get; }
        ICourseRepository Course { get; }
        ICourseItemRepository CourseItem { get; }

        //Global methods
        void Save();
    }
}

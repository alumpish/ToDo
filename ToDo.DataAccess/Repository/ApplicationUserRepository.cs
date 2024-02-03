using ToDo.DataAccess.Data;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    public class ApplicationUserRepository : Repository<ApplicationUser>
    {
        private ApplicationDbContext _db;
        public ApplicationUserRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

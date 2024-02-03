using ToDo.DataAccess.Data;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    public class ToDoGroupRepository : Repository<ToDoGroup>
    {
        private readonly ApplicationDbContext _db;

        public ToDoGroupRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

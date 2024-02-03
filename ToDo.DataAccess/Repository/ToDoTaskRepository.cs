using ToDo.DataAccess.Data;
using ToDo.Models;

namespace ToDo.DataAccess.Repository
{
    public class ToDoTaskRepository : Repository<ToDoTask>
    {
        private readonly ApplicationDbContext _db;

        public ToDoTaskRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    }
}

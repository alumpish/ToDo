using ToDo.DataAccess.Data;

namespace ToDo.DataAccess.Repository
{
    public class UnitOfWork
    {
        private ApplicationDbContext _db;
        public ToDoGroupRepository ToDoGroup { get; private set; }
        public ToDoTaskRepository ToDoTask { get; private set; }
        public ApplicationUserRepository ApplicationUser { get; private set; }


        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            ToDoGroup = new ToDoGroupRepository(_db);
            ToDoTask = new ToDoTaskRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
        }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}

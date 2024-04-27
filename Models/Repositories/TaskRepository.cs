using Microsoft.EntityFrameworkCore;

namespace ProjectManager1.Models.Repositories
{
    public class TaskRepository : IProjectManagerRepository<Task>
    {
        ProjectManagerDbContext db;

        public TaskRepository (ProjectManagerDbContext _db)
        {
            this.db = _db;
        }

        public void Add(Task entity)
        {
            db.Tasks.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var task = Find(id);
            db.Tasks.Remove(task);
            db.SaveChanges();
        }

        public Task Find(int id)
        {

            var task = db.Tasks.Include(s => s.Status).Include(p => p.Project).SingleOrDefault(t => t.Id == id);
            return task;
        }

        public IList<Task> List()
        {
            return db.Tasks.Include(p => p.Project).Include(s => s.Status).ToList();
        }

        public void Update(int id, Task newTask)
        {
            db.Update(newTask);
            db.SaveChanges();
        }
    }
}

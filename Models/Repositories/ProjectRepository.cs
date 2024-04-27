using Microsoft.EntityFrameworkCore;

namespace ProjectManager1.Models.Repositories
{
    public class ProjectRepository : IProjectManagerRepository<Project>
    {
        ProjectManagerDbContext db;

        public ProjectRepository(ProjectManagerDbContext _db)
        {
            this.db = _db;
        }

        public void Add(Project entity)
        {
            db.Projects.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var project = Find(id);
            db.Projects.Remove(project);
            db.SaveChanges();
        }

        public Project Find(int id)
        {
            var project = db.Projects.Include(s => s.Status).Include(p => p.Tasks).SingleOrDefault(p => p.Id == id);
            return project;
        }

        public IList<Project> List()
        {
            return db.Projects.Include(s => s.Status).Include(p => p.Tasks).ToList();
        }

        public void Update(int id, Project newProject)
        {
            db.Update(newProject);
            db.SaveChanges();
        }
    }
}

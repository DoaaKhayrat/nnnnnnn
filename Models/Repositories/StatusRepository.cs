namespace ProjectManager1.Models.Repositories
{
    public class StatusRepository
     : IProjectManagerRepository<Status>
    {

        ProjectManagerDbContext db;
        public StatusRepository(ProjectManagerDbContext _db)
        {
            db = _db;
        }

        public void Add(Status entity)
        {
            db.Statuses.Add(entity);
            db.SaveChanges();
        }

        public void Delete(int id)
        {
            var status = Find(id);
            db.Statuses.Remove(status);
            db.SaveChanges();
        }

        public Status Find(int id)
        {
            var status = db.Statuses.SingleOrDefault(s => s.Id == id);
            return status;
        }

        public IList<Status> List()
        {
            return db.Statuses.ToList();
        }

        public void Update(int id, Status newStatus)
        {
            db.Update(newStatus);
            db.SaveChanges();
        }
    }
}

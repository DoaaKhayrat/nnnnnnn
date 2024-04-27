namespace ProjectManager1.Models.Repositories
{
    public interface IProjectManagerRepository<TEntity>
    {
        IList<TEntity> List();
        TEntity Find(int id);
        void Add(TEntity entity);
        void Update(int id,TEntity entity);
        void Delete(int id);
    }
}

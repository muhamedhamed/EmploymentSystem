namespace EmploymentSystem.Domain.Interfaces.Repositories;


public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity GetById(int id);
    IEnumerable<TEntity> GetAll();
    void Add(TEntity entity);
    void Update(TEntity entity);
    void Remove(TEntity entity);
}

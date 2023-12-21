namespace EmploymentSystem.Domain.Interfaces.Repositories;


public interface IGenericRepository<TEntity> where TEntity : class
{
    TEntity GetById(string id);
    IEnumerable<TEntity> GetAll();
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    void Remove(TEntity entity);
}

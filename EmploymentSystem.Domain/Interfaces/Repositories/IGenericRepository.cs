namespace EmploymentSystem.Domain.Interfaces.Repositories;


public interface IGenericRepository<TEntity> where TEntity : class
{
    Task<TEntity> GetByIdAsync(string id);
    Task<IEnumerable<TEntity>> GetAllAsync();
    Task<TEntity> AddAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
    Task RemoveAsync(TEntity entity);
}

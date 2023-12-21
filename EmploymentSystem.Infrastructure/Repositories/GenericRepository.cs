using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories;

public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private readonly DbSet<TEntity> _dbSet;

    public GenericRepository(AppDbContext dbContext)
    {
        _dbContext = dbContext;
        _dbSet = dbContext.Set<TEntity>();
    }
    public TEntity Add(TEntity entity)
    {
        _dbSet.Add(entity);
        return entity;
    }

    public IEnumerable<TEntity> GetAll()
    {
        return _dbSet.ToList();
    }

    public TEntity GetById(string id)
    {

#pragma warning disable CS8603 // Possible null reference return.
        return _dbSet.Find(id);
#pragma warning restore CS8603 // Possible null reference return.

    }

    public void Remove(TEntity entity)
    {
        // var found = _dbContext.Set<TEntity>().Find(entity);
        // if (found == null)
        //     return false;

        _dbContext.Set<TEntity>().Remove(entity);
        // return true;
    }

    public TEntity Update(TEntity entity)
    {
        // _dbSet.Attach(entity);
        _dbContext.Entry(entity).State = EntityState.Modified;
        return entity;
    }
}

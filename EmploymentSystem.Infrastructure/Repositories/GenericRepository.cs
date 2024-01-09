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

    public async Task<IEnumerable<TEntity>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public async Task<TEntity> GetByIdAsync(string id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<TEntity> AddAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        // not imprtant to make the update async 
        // check again
        _dbContext.Entry(entity).State = EntityState.Modified;
        return entity;
    }

    public void Remove(TEntity entity)
    {
        //_dbSet.Remove(entity);
        _dbContext.Set<TEntity>().Remove(entity);
    }
}

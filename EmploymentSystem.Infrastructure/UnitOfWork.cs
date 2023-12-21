using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EmploymentSystem.Infrastructure;

public class UnitOfWork<TEntity> : IUnitOfWork<TEntity> where TEntity : class
{
    private readonly AppDbContext _dbContext;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext dbContext,
                         IGenericRepository<TEntity> repository,
                         IUserRepository userRepository,
                         IGenericRepository<Vacancy> vacancyRepository,
                         IGenericRepository<ApplicationVacancy> applicationVacancyRepository)
    {
        _dbContext = dbContext;
        Repository =repository;
        UserRepository = userRepository;
        VacancyRepository = vacancyRepository;
        ApplicationVacancyRepository = applicationVacancyRepository;
    }

    public IGenericRepository<TEntity> Repository { get; }
    public IUserRepository UserRepository { get; }
    public IGenericRepository<Vacancy> VacancyRepository { get; }
    public IGenericRepository<ApplicationVacancy> ApplicationVacancyRepository { get; }

    IGenericRepository<TEntity> IUnitOfWork<TEntity>.VacancyRepository => throw new NotImplementedException();

    IGenericRepository<TEntity> IUnitOfWork<TEntity>.ApplicationVacancyRepository => throw new NotImplementedException();

    public void BeginTransaction()
    {
        _transaction = _dbContext.Database.BeginTransaction();
    }
    public void SaveChanges()
    {
        _dbContext.SaveChanges();
    }
    public void CommitTransaction()
    {
        _transaction.Commit();
        _transaction.Dispose();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}

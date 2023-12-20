using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Storage;

namespace EmploymentSystem.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private IDbContextTransaction _transaction;

    public UnitOfWork(AppDbContext dbContext,
                         IGenericRepository<User> userRepository,
                         IGenericRepository<Vacancy> vacancyRepository,
                         IGenericRepository<ApplicationVacancy> applicationVacancyRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        VacancyRepository = vacancyRepository;
        ApplicationVacancyRepository = applicationVacancyRepository;
    }

    // public IUserRepository UserRepository { get; }
    public IGenericRepository<User> UserRepository { get; }
    public IGenericRepository<Vacancy> VacancyRepository { get; }
    public IGenericRepository<ApplicationVacancy> ApplicationVacancyRepository { get; }


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

using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Storage;

namespace EmploymentSystem.Infrastructure;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private IDbContextTransaction _transaction;

    public IUserRepository UserRepository { get; }
    public IVacancyRepository VacancyRepository { get; }
    public IApplicationVacancyRepository ApplicationVacancyRepository { get; }

    public UnitOfWork(
                        AppDbContext dbContext,
                        IUserRepository userRepository,
                        IVacancyRepository vacancyRepository,
                        IApplicationVacancyRepository applicationVacancyRepository)
    {
        _dbContext = dbContext;
        UserRepository = userRepository;
        VacancyRepository = vacancyRepository;
        ApplicationVacancyRepository = applicationVacancyRepository;
    }

    public async Task BeginTransactionAsync()
    {
        _transaction =await _dbContext.Database.BeginTransactionAsync();
    }

    public async Task SaveChangesAsync()
    {
        await _dbContext.SaveChangesAsync();
    }
    public async Task CommitTransactionAsync()
    {
        await _transaction.CommitAsync();
        _transaction.Dispose();
    }

    public void Dispose()
    {
        _dbContext.Dispose();
    }
}

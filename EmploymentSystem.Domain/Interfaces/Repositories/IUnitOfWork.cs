using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork<TEntity> where TEntity : class
    {
        public IGenericRepository<TEntity> Repository { get; }
        public IUserRepository UserRepository { get; }
        public IGenericRepository<TEntity> VacancyRepository { get; }
        public IGenericRepository<TEntity> ApplicationVacancyRepository { get; }
        void BeginTransaction();
        void SaveChanges();
        void CommitTransaction();
    }
}

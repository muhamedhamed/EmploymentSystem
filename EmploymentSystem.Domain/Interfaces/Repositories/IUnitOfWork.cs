using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        public IGenericRepository<User> UserRepository { get; }
        public IGenericRepository<Vacancy> VacancyRepository { get; }
        public IGenericRepository<ApplicationVacancy> ApplicationVacancyRepository { get; }
        void BeginTransaction();
        void SaveChanges();
        void CommitTransaction();
    }
}

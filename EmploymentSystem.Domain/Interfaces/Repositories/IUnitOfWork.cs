using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork :IDisposable
    {
        public IUserRepository UserRepository { get; }
        public IVacancyRepository VacancyRepository { get; }
        public IApplicationVacancyRepository ApplicationVacancyRepository { get; }
        void BeginTransaction();
        void SaveChanges();
        void CommitTransaction();
    }
}

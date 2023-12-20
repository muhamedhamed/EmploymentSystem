using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories;

public interface IVacancyRepository : IGenericRepository<Vacancy>
{
    // IEnumerable<Vacancy> GetActiveVacancies();
    // IEnumerable<Vacancy> GetVacanciesByEmployer(int employerId);
}

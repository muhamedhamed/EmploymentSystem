using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories;

public interface IVacancyRepository : IGenericRepository<Vacancy>
{
    Task<IEnumerable<ApplicationVacancy>> GetApplicationsByVacancyAsync(string vacancyId);
    // IEnumerable<Vacancy> GetActiveVacancies();
    // IEnumerable<Vacancy> GetVacanciesByEmployer(int employerId);
}

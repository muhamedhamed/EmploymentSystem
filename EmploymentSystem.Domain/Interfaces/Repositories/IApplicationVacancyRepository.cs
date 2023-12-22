using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories;

public interface IApplicationVacancyRepository : IGenericRepository<ApplicationVacancy>
{
    IEnumerable<ApplicationVacancy> GetApplicationsByVacancy(string vacancyId);
    // IEnumerable<ApplicationVacancy> GetApplicationsByApplicant(int applicantId);
}

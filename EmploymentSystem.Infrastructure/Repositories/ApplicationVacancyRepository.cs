using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;

namespace EmploymentSystem.Infrastructure.Repositories;

public class ApplicationVacancyRepository : GenericRepository<ApplicationVacancy>, IApplicationVacancyRepository
{
    private readonly AppDbContext _context;

    public ApplicationVacancyRepository(AppDbContext context) :base(context)
    {
        _context = context;
    }

    // public IEnumerable<ApplicationVacancy> GetApplicationsByApplicant(int applicantId)
    // {
    //     throw new NotImplementedException();
    // }

    // public IEnumerable<ApplicationVacancy> GetApplicationsByVacancy(int vacancyId)
    // {
    //     // Implement logic to get applications by vacancy ID from the database
    //     return _context.ApplicationVacancies.Where(app => app.VacancyId == vacancyId).ToList();
    // }
}


using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories;

public class VacancyRepository : GenericRepository<Vacancy>, IVacancyRepository
{
private readonly AppDbContext _context;

    public VacancyRepository(AppDbContext context) :base(context)
    {
        _context = context;
    }

    public IEnumerable<ApplicationVacancy> GetApplicationsByVacancy(string vacancyId)
    {
        var vacancies = _context.Vacancies
            .Include(v => v.Applications)
            .Where(v => v.VacancyId == vacancyId)
            .SelectMany(v => v.Applications)
            .ToList();

        return vacancies;
    }
    
//     public IEnumerable<Vacancy> GetActiveVacancies()
//     {
//         // Implement logic to get active vacancies from the database
//         return _context.Vacancies.Where(v => !v.IsExpired).ToList();
//     }
}

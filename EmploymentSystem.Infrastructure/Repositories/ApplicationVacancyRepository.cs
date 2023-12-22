using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories;

public class ApplicationVacancyRepository : GenericRepository<ApplicationVacancy>, IApplicationVacancyRepository
{
    private readonly AppDbContext _context;

    public ApplicationVacancyRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    // public IEnumerable<ApplicationVacancy> GetApplicationsByApplicant(int applicantId)
    // {
    //     throw new NotImplementedException();
    // }
}


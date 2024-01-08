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

    //public async Task<IEnumerable<ApplicationVacancy>> GetApplicationsByApplicantAsync(int applicantId)
    //{
    //    return await _context.ApplicationVacancies
    //        .Where(app => app.ApplicantId == applicantId)
    //        .ToListAsync();
    //}
}


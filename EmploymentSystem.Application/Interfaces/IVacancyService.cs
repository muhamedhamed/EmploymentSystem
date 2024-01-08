using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IVacancyService
{
    Task<VacancyDto> GetVacancyByIdAsync(string vacancyId);
    Task<IEnumerable<VacancyDto>> GetAllVacanciesAsync();
    Task<VacancyDto> CreateVacancyAsync(VacancyDto vacancy);
    Task<VacancyDto> UpdateVacancyAsync(VacancyDto vacancyDto, string vacancyId);
    Task DeleteVacancyAsync(string vacancyId);

    Task<IEnumerable<ApplicationVacancyDto>> GetApplicationsByVacancyAsync(string vacancyId);
    // Task<IEnumerable<VacancyDto>> GetActiveVacanciesAsync();
    // Task<IEnumerable<VacancyDto>> GetVacanciesByEmployerAsync(int employerId);
}

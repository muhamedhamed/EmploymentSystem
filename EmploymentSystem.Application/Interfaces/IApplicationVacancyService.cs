using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IApplicationVacancyService
{
    Task<ApplicationVacancyDto> GetApplicationByIdAsync(string applicationId);
    Task<ApplicationVacancyDto?> ApplyForVacancyAsync(ApplicationVacancyDto applicationDto);
    Task<ApplicationVacancyDto> UpdateApplicationAsync(ApplicationVacancyDto applicationDto, string applicationId);
    Task WithdrawApplicationAsync(string applicationId);

    // Task<IEnumerable<ApplicationVacancyDto>> GetApplicationsByVacancyAsync(int vacancyId);
    // Task<IEnumerable<ApplicationVacancyDto>> GetApplicationsByApplicantAsync(int applicantId);
    // Task<IEnumerable<ApplicationVacancyDto>> GetApplicationsByUserAsync(int userId);
}

using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IApplicationVacancyService
{
    void ApplyForVacancy(ApplicationVacancyDto applicationDto);
    ApplicationVacancyDto GetApplicationById(int applicationId);
    void UpdateApplication(ApplicationVacancyDto applicationDto);
    void WithdrawApplication(int applicationId);

    // IEnumerable<ApplicationVacancyDto> GetApplicationsByVacancy(int vacancyId);
    // IEnumerable<ApplicationVacancyDto> GetApplicationsByApplicant(int applicantId);
    // IEnumerable<ApplicationVacancyDto> GetApplicationsByUser(int userId);
    // // Other methods related to application vacancy management...
}

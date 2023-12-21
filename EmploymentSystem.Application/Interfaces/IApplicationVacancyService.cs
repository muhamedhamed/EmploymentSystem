using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IApplicationVacancyService
{
    ApplicationVacancyDto GetApplicationById(string applicationId);
    ApplicationVacancyDto ApplyForVacancy(ApplicationVacancyDto applicationDto);
    ApplicationVacancyDto UpdateApplication(ApplicationVacancyDto applicationDto,string applicationId);
    void WithdrawApplication(string applicationId);

    // IEnumerable<ApplicationVacancyDto> GetApplicationsByVacancy(int vacancyId);
    // IEnumerable<ApplicationVacancyDto> GetApplicationsByApplicant(int applicantId);
    // IEnumerable<ApplicationVacancyDto> GetApplicationsByUser(int userId);
    // // Other methods related to application vacancy management...
}

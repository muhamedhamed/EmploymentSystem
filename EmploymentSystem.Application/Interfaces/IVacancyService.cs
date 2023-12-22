using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IVacancyService
{
    VacancyDto GetVacancyById(string vacancyId);
    IEnumerable<VacancyDto> GetAllVacancies();
    VacancyDto CreateVacancy(VacancyDto vacancy);
    VacancyDto UpdateVacancy(VacancyDto vacancyDto, string vacancyId);
    void DeleteVacancy(string vacancyId);

    IEnumerable<ApplicationVacancyDto> GetApplicationsByVacancy(string vacancyId);
    // IEnumerable<VacancyDto> GetActiveVacancies();
    // IEnumerable<VacancyDto> GetVacanciesByEmployer(int employerId);
    // // Other methods related to vacancy management...
}

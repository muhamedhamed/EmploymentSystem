using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IVacancyService
{
    void CreateVacancy(VacancyDto vacancy);
    VacancyDto GetVacancyById(int vacancyId);
    IEnumerable<VacancyDto> GetAllVacancies();
    void UpdateVacancy(VacancyDto vacancyDto);
    void DeleteVacancy(int vacancyId);
    // IEnumerable<VacancyDto> GetActiveVacancies();
    // IEnumerable<VacancyDto> GetVacanciesByEmployer(int employerId);
    // // Other methods related to vacancy management...
}

namespace EmploymentSystem.Application.Dtos;

public class ApplicationVacancyDto
{
    public string ApplicationVacancyId { get; set; }
    public string ApplicantId { get; set; }
    public string VacancyId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

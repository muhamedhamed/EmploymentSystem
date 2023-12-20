namespace EmploymentSystem.Application.Dtos;

public class ApplicationVacancyDto
{
    public int ApplicationId { get; set; }
    public int ApplicantId { get; set; }
    public int VacancyId { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

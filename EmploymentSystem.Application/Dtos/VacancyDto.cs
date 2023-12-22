namespace EmploymentSystem.Application.Dtos;

public class VacancyDto
{
    public string VacancyId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public decimal Salary { get; set; }
    public string Location { get; set; }
    public int MaxApplications { get; set; }
    public string EmployerId { get; set; }
    public DateTime ExpiryDate { get; set; }
    public bool IsExpired { get; set; }
    public bool IsActive { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
}

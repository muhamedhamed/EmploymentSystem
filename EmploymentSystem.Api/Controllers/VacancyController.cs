using System.Runtime.InteropServices.ComTypes;
using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

[ApiController]
[Route("api/vacancies")]
[Authorize(AuthenticationSchemes = "Bearer")]
public class VacancyController : ControllerBase
{
    private readonly IVacancyService _vacancyService;
    private readonly ILogger<VacancyController> _logger;

    public VacancyController(
        IVacancyService vacancyService,
        ILogger<VacancyController> logger)
    {
        _vacancyService = vacancyService;
        _logger = logger;
    }

    [HttpGet]
    [ActionName("GetAllVacancies")]
    public IActionResult GetAllVacancies()
    {
        try
        {
            _logger.LogInformation("Attempting to retrieve all vacancies.");

            var vacanciesDto = _vacancyService.GetAllVacancies();
            if (vacanciesDto == null || !vacanciesDto.Any())
            {
                _logger.LogWarning("No vacancies found.");
                return NotFound("No vacancies found.");
            }
            _logger.LogInformation("Successfully retrieved all vacancies.");

            return Ok(vacanciesDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving vacancies: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpGet("{vacancyId}")]
    [ActionName("GetVacancyById")]
    [Authorize(Roles = "Employer")]
    public IActionResult GetVacancyById(string vacancyId)
    {
        try
        {
            _logger.LogInformation($"Attempting to retrieve vacancy with ID: {vacancyId}");

            if (!Guid.TryParse(vacancyId, out _))
            {
                _logger.LogError("Invalid vacancy ID format.");
                return BadRequest("Invalid vacancy ID format.");
            }

            var vacancyDto = _vacancyService.GetVacancyById(vacancyId);
            if (vacancyDto == null)
            {
                _logger.LogWarning($"Vacancy with ID: {vacancyId} not found.");
                return NotFound();
            }

            _logger.LogInformation($"Successfully retrieved vacancy with ID: {vacancyId}");
            return Ok(vacancyDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving vacancy with ID {vacancyId}: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpPost]
    [ActionName("CreateVacancy")]
    [Authorize(Roles = "Employer")]
    public IActionResult CreateVacancy([FromBody] VacancyDto vacancyDto)
    {
        try
        {
            _logger.LogInformation($"Attempting to create a new vacancy: {vacancyDto.Title}");

            // Validate if the request body is null or empty
            if (vacancyDto == null)
            {
                _logger.LogError("Invalid vacancy data. Request body cannot be null.");
                return BadRequest("Invalid vacancy data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }

            var result = _vacancyService.CreateVacancy(vacancyDto);

            _logger.LogInformation($"Successfully created a new vacancy with ID: {result.VacancyId}");
            return CreatedAtAction(nameof(CreateVacancy), result);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to add vacancy: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpPut("{vacancyId}")]
    [ActionName("UpdateVacancy")]
    [Authorize(Roles = "Employer")]
    public IActionResult UpdateVacancy(string vacancyId, [FromBody] VacancyDto vacancyDto)
    {
        try
        {
            _logger.LogInformation($"Attempting to update vacancy with ID: {vacancyId}");

            if (!Guid.TryParse(vacancyId, out _))
            {
                _logger.LogError("Invalid vacancy ID format.");
                return BadRequest("Invalid vacancy ID format.");
            }

            // Check if the request body is null
            if (vacancyDto == null)
            {
                _logger.LogError("Invalid vacancy data. Request body cannot be null.");
                return BadRequest("Invalid vacancy data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }

            // Check if the vacancy exists
            var existingVacancy = _vacancyService.GetVacancyById(vacancyId);
            if (existingVacancy == null)
            {
                _logger.LogError($"Vacancy with ID {vacancyId} not found.");
                return NotFound($"Vacancy with ID {vacancyId} not found.");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingVacancy.EmployerId != userId)
            {
                _logger.LogError("User does not have permission to update this vacancy.");
                return Forbid("User does not have permission to update this vacancy.");
            }

            // Update the vacancy
            _vacancyService.UpdateVacancy(vacancyDto, vacancyId);

            _logger.LogInformation($"Successfully updated vacancy with ID: {vacancyId}");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update vacancy: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpDelete("{vacancyId}")]
    [ActionName("DeleteVacancy")]
    [Authorize(Roles = "Employer")]
    public IActionResult DeleteVacancy(string vacancyId)
    {
        try
        {
            _logger.LogInformation($"Attempting to delete vacancy with ID: {vacancyId}");

            if (!Guid.TryParse(vacancyId, out _))
            {
                _logger.LogError("Invalid vacancy ID format.");
                return BadRequest("Invalid vacancy ID format.");
            }

            // Check if the vacancy exists
            var existingVacancy = _vacancyService.GetVacancyById(vacancyId);
            if (existingVacancy == null)
            {
                _logger.LogError($"Vacancy with ID {vacancyId} not found.");
                return NotFound($"Vacancy with ID {vacancyId} not found.");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingVacancy.EmployerId != userId)
            {
                _logger.LogError("User does not have permission to delete this vacancy.");
                return Forbid("User does not have permission to delete this vacancy.");
            }

            // Delete the vacancy
            _vacancyService.DeleteVacancy(vacancyId);

            _logger.LogInformation($"Successfully deleted vacancy with ID: {vacancyId}");
            return NoContent();

        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete vacancy: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpGet("{vacancyId}/applications")]
    [ActionName("GetApplicationsPerVacancy")]
    [Authorize(Roles = "Employer")]
    public IActionResult GetApplicationsPerVacancy(string vacancyId)
    {
        try
        {
            if (!Guid.TryParse(vacancyId, out _))
            {
                _logger.LogError("Invalid vacancy ID format.");
                return BadRequest("Invalid vacancy ID format.");
            }

            _logger.LogInformation($"Attempting to retrieve list of applications per vacancy with vecency ID: {vacancyId}");

            var vacancyDto = _vacancyService.GetVacancyById(vacancyId);
            if (vacancyDto == null)
            {
                _logger.LogWarning($"Vacancy with ID: {vacancyId} not found.");
                return NotFound();
            }
            _logger.LogInformation($"Successfully retrieved vacancy with ID: {vacancyId}");

            var applications = _vacancyService.GetApplicationsByVacancy(vacancyId);
            if (applications == null)
            {
                _logger.LogWarning($"Vacancy with ID: {vacancyId} hasn't application yet.");
                return NotFound();
            }

            _logger.LogInformation($"Successfully retrieved applications list for vacancy with ID: {vacancyId}");

            return Ok(applications);
        }
        catch (Exception ex)
        {
            _logger.LogError($"An error occurred while retrieving vacancy with ID {vacancyId}: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }
}

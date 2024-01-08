using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationVacancyController : ControllerBase
{
    private readonly IApplicationVacancyService _applicationVacancyService;
    private readonly ILogger<ApplicationVacancyController> _logger;

    public ApplicationVacancyController(
                IApplicationVacancyService applicationVacancyService,
                ILogger<ApplicationVacancyController> logger
        )
    {
        _applicationVacancyService = applicationVacancyService;
        _logger = logger;
    }

    [HttpGet("{applicationId}")]
    [ActionName("GetApplicationById")]
    public async Task<IActionResult> GetApplicationById(string applicationId)
    {
        try
        {
            if (string.IsNullOrEmpty(applicationId))
            {
                _logger.LogError("Invalid applicationId. It cannot be null or empty.");
                return BadRequest("Invalid applicationId.");
            }
            var applicationDto = await _applicationVacancyService.GetApplicationByIdAsync(applicationId);
            if (applicationDto == null)
            {
                _logger.LogError($"Application with Id : {applicationDto.ApplicationVacancyId} Not Found");
                return NotFound("Application not found.");
            }
            _logger.LogInformation($"Successfully Get Application with Id : {applicationDto.ApplicationVacancyId}");
            return Ok(applicationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Error while getting application by Id: {applicationId}. Error: {ex.Message}");
            return StatusCode(500, "Internal Server Error");
        }
    }

    [HttpPost]
    [ActionName("ApplyForVacancy")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    [Authorize(Roles = "Applicant")]
    public async Task<IActionResult> ApplyForVacancy([FromBody] ApplicationVacancyDto applicationDto)
    {
        try
        {
            // Validate if the request body is null or empty
            if (applicationDto == null)
            {
                _logger.LogError("Invalid application data. Request body cannot be null.");
                return BadRequest("Invalid application data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }

            // Apply for vacancy and return CreatedAtAction result if successful
            var result = await _applicationVacancyService.ApplyForVacancyAsync(applicationDto);
                        // Validate if the request body is null or empty
            if (result == null)
            {
                _logger.LogError("Vacancy Reached the Max allowed number of applications.");
                return BadRequest("Vacancy Reached the Max allowed number of applications.");
            }
            
            _logger.LogInformation("Application Added successfully.");
            return CreatedAtAction(nameof(ApplyForVacancy), applicationDto);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to add application: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpPut("{applicationId}")]
    [ActionName("UpdateApplication")]
    [Authorize(Roles = "Applicant")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> UpdateApplication(string applicationId, [FromBody] ApplicationVacancyDto applicationDto)
    {
        try
        {
            if (!Guid.TryParse(applicationId, out _))
            {
                _logger.LogError("Invalid application ID format.");
                return BadRequest("Invalid application ID format.");
            }

            // Validate if the request body is null or empty
            if (applicationDto == null)
            {
                _logger.LogError("Invalid application data. Request body cannot be null.");
                return BadRequest("Invalid application data. Request body cannot be null.");
            }

            // Validate using model state
            if (!ModelState.IsValid)
            {
                _logger.LogError("Invalid model state. Check the request data.");
                return BadRequest(ModelState);
            }

            var existingApplication = await _applicationVacancyService.GetApplicationByIdAsync(applicationId);
            if (existingApplication == null)
            {
                _logger.LogError($"Application with ID {applicationId} not found.");
                return NotFound($"Application with ID {applicationId} not found.");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingApplication.ApplicantId != userId)
            {
                _logger.LogError("User does not have permission to update this application.");
                return Forbid("User does not have permission to update this application.");
            }

            await _applicationVacancyService.UpdateApplicationAsync(applicationDto, applicationId);
            _logger.LogInformation("Application updated successfully.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to update application: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }

    [HttpDelete("{applicationId}")]
    [ActionName("WithdrawApplication")]
    [Authorize(Roles = "Applicant")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public async Task<IActionResult> WithdrawApplication(string applicationId)
    {
        try
        {
            if (!Guid.TryParse(applicationId, out _))
            {
                _logger.LogError("Invalid application ID format.");
                return BadRequest("Invalid application ID format.");
            }

            var existingApplication =await _applicationVacancyService.GetApplicationByIdAsync(applicationId);
            if (existingApplication == null)
            {
                _logger.LogError($"Application with ID {applicationId} not found.");
                return NotFound($"Application with ID {applicationId} not found.");
            }

            var userId = User.Claims.FirstOrDefault(c => c.Type == "userId")?.Value;
            if (existingApplication.ApplicantId != userId)
            {
                _logger.LogError("User does not have permission to update this application.");
                return Forbid("User does not have permission to update this application.");
            }

            await _applicationVacancyService.WithdrawApplicationAsync(applicationId);
            _logger.LogInformation("Application deleted successfully.");
            return NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError($"Failed to delete application: {ex.Message}");
            return BadRequest("Internal Server Error");
        }
    }
}

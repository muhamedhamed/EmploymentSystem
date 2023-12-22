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
    public IActionResult GetApplicationById(string applicationId)
    {
        try
        {
            if (string.IsNullOrEmpty(applicationId))
            {
                _logger.LogError("Invalid applicationId. It cannot be null or empty.");
                return BadRequest("Invalid applicationId.");
            }
            var applicationDto = _applicationVacancyService.GetApplicationById(applicationId);
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
    [Authorize(Roles = "Applicant")]
    public IActionResult ApplyForVacancy([FromBody] ApplicationVacancyDto applicationDto)
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
            var result = _applicationVacancyService.ApplyForVacancy(applicationDto);
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
    public IActionResult UpdateApplication(string applicationId, [FromBody] ApplicationVacancyDto applicationDto)
    {
        // if (applicationId != applicationDto.ApplicationId)
        // {
        //     return BadRequest();
        // }

        _applicationVacancyService.UpdateApplication(applicationDto, applicationId);
        return NoContent();
    }

    [HttpDelete("{applicationId}")]
    [ActionName("WithdrawApplication")]
    public IActionResult WithdrawApplication(string applicationId)
    {
        _applicationVacancyService.WithdrawApplication(applicationId);
        return NoContent();
    }

    // [HttpGet("vacancy/{vacancyId}")]
    // public IActionResult GetApplicationsByVacancy(int vacancyId)
    // {
    //     var applicationsDto = _applicationVacancyService.GetApplicationsByVacancy(vacancyId);
    //     return Ok(applicationsDto);
    // }

    // [HttpGet("user/{userId}")]
    // public IActionResult GetApplicationsByUser(int userId)
    // {
    //     var applicationsDto = _applicationVacancyService.GetApplicationsByUser(userId);
    //     return Ok(applicationsDto);
    // }
}

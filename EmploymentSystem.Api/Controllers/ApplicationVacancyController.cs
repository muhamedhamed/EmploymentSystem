using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

[ApiController]
[Route("api/applications")]
// [Authorize(Roles = "Applicant")]
public class ApplicationVacancyController : ControllerBase
{
    private readonly IApplicationVacancyService _applicationVacancyService;

    public ApplicationVacancyController(IApplicationVacancyService applicationVacancyService)
    {
        _applicationVacancyService = applicationVacancyService;
    }

    [HttpGet("{applicationId}")]
    [ActionName("GetApplicationById")]
    public IActionResult GetApplicationById(string applicationId)
    {
        var applicationDto = _applicationVacancyService.GetApplicationById(applicationId);
        if (applicationDto == null)
        {
            return NotFound();
        }
        return Ok(applicationDto);
    }

    [HttpPost]
    [ActionName("ApplyForVacancy")]
    public IActionResult ApplyForVacancy([FromBody] ApplicationVacancyDto applicationDto)
    {
        try
        {
            var result = _applicationVacancyService.ApplyForVacancy(applicationDto);
            return CreatedAtAction(nameof(ApplyForVacancy), applicationDto);
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            return BadRequest();

            // Throw an exception to indicate failure
            throw new Exception("Failed to add application: " + ex.Message);
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

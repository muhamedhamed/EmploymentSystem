using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

[ApiController]
[Route("api/applications")]
public class ApplicationVacancyController : ControllerBase
{
    private readonly IApplicationVacancyService _applicationVacancyService;

    public ApplicationVacancyController(IApplicationVacancyService applicationVacancyService)
    {
        _applicationVacancyService = applicationVacancyService;
    }

    [HttpGet("{applicationId}")]
    public IActionResult GetApplicationById(int applicationId)
    {
        var applicationDto = _applicationVacancyService.GetApplicationById(applicationId);
        if (applicationDto == null)
        {
            return NotFound();
        }
        return Ok(applicationDto);
    }

    [HttpPost]
    public IActionResult ApplyForVacancy([FromBody] ApplicationVacancyDto applicationDto)
    {
        _applicationVacancyService.ApplyForVacancy(applicationDto);
        return CreatedAtAction(nameof(GetApplicationById), new { applicationId = applicationDto.ApplicationId }, applicationDto);
    }

    [HttpPut("{applicationId}")]
    public IActionResult UpdateApplication(int applicationId, [FromBody] ApplicationVacancyDto applicationDto)
    {
        if (applicationId != applicationDto.ApplicationId)
        {
            return BadRequest();
        }

        _applicationVacancyService.UpdateApplication(applicationDto);
        return NoContent();
    }

    [HttpDelete("{applicationId}")]
    public IActionResult WithdrawApplication(int applicationId)
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

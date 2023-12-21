using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

[ApiController]
[Route("api/vacancies")]
[Authorize(Roles = "Employer")]
public class VacancyController : ControllerBase
{
    private readonly IVacancyService _vacancyService;

    public VacancyController(IVacancyService vacancyService)
    {
        _vacancyService = vacancyService;
    }

    [HttpGet]
    [ActionName("GetAllVacancies")]
    public IActionResult GetAllVacancies()
    {
        var vacanciesDto = _vacancyService.GetAllVacancies();
        return Ok(vacanciesDto);
    }

    [HttpGet("{vacancyId}")]
    [ActionName("GetVacancyById")]
    public IActionResult GetVacancyById(string vacancyId)
    {
        var vacancyDto = _vacancyService.GetVacancyById(vacancyId);
        if (vacancyDto == null)
        {
            return NotFound();
        }
        return Ok(vacancyDto);
    }

    [HttpPost]
    [ActionName("CreateVacancy")]
    public IActionResult CreateVacancy([FromBody] VacancyDto vacancyDto)
    {
        try
        {
            var result =_vacancyService.CreateVacancy(vacancyDto);
            return CreatedAtAction(nameof(CreateVacancy), vacancyDto);
        }
        catch (Exception ex)
        {
            // Log the exception if needed
            return BadRequest();

            // Throw an exception to indicate failure
            throw new Exception("Failed to add vacancy: " + ex.Message);
        }
    }

    [HttpPut("{vacancyId}")]
    [ActionName("UpdateVacancy")]
    public IActionResult UpdateVacancy(string vacancyId, [FromBody] VacancyDto vacancyDto)
    {
        //need to add check for the existance of the vacancy
        _vacancyService.UpdateVacancy(vacancyDto,vacancyId);
        return NoContent();
    }

    [HttpDelete("{vacancyId}")]
    [ActionName("DeleteVacancy")]
    public IActionResult DeleteVacancy(string vacancyId)
    {
        _vacancyService.DeleteVacancy(vacancyId);
        return NoContent();
    }
}

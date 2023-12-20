using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EmploymentSystem.Api.Controllers;

    [ApiController]
    [Route("api/vacancies")]
    public class VacancyController : ControllerBase
    {
       private readonly IVacancyService _vacancyService;

        public VacancyController(IVacancyService vacancyService)
        {
            _vacancyService = vacancyService;
        }

        [HttpGet("{vacancyId}")]
        public IActionResult GetVacancyById(int vacancyId)
        {
            var vacancyDto = _vacancyService.GetVacancyById(vacancyId);
            if (vacancyDto == null)
            {
                return NotFound();
            }
            return Ok(vacancyDto);
        }

        [HttpGet]
        public IActionResult GetAllVacancies()
        {
            var vacanciesDto = _vacancyService.GetAllVacancies();
            return Ok(vacanciesDto);
        }

        [HttpPost]
        public IActionResult CreateVacancy([FromBody] VacancyDto vacancyDto)
        {
            _vacancyService.CreateVacancy(vacancyDto);
            return CreatedAtAction(nameof(GetVacancyById), new { vacancyId = vacancyDto.VacancyId }, vacancyDto);
        }

        [HttpPut("{vacancyId}")]
        public IActionResult UpdateVacancy(int vacancyId, [FromBody] VacancyDto vacancyDto)
        {
            if (vacancyId != vacancyDto.VacancyId)
            {
                return BadRequest();
            }

            _vacancyService.UpdateVacancy(vacancyDto);
            return NoContent();
        }

        [HttpDelete("{vacancyId}")]
        public IActionResult DeleteVacancy(int vacancyId)
        {
            _vacancyService.DeleteVacancy(vacancyId);
            return NoContent();
        }
}

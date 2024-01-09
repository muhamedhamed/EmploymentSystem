using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;

namespace EmploymentSystem.Application.Services;

public class ApplicationVacancyService : IApplicationVacancyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    private readonly IVacancyService _vacancyService;

    public ApplicationVacancyService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IVacancyService vacancyService)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _vacancyService = vacancyService;
    }

    public async Task<ApplicationVacancyDto?> ApplyForVacancyAsync(ApplicationVacancyDto applicationDto)
    {
        var applicationEntity = _mapper.Map<ApplicationVacancy>(applicationDto);

        //That part of the code need to be more organized
        var vacancyEntity = await _unitOfWork.VacancyRepository.GetByIdAsync(applicationEntity.VacancyId);

        if (vacancyEntity == null)
        {
            return null;
        }

        var applicationsByVacancyList = await _vacancyService.GetApplicationsByVacancyAsync(vacancyEntity.VacancyId);

        int numberOfApplication = applicationsByVacancyList.Count();

        if (applicationsByVacancyList != null && numberOfApplication < vacancyEntity.MaxApplications)
        {
            applicationEntity.CreatedAt = DateTime.Now;
            applicationEntity.UpdatedAt = DateTime.Now;

            await _unitOfWork.ApplicationVacancyRepository.AddAsync(applicationEntity);
            await _unitOfWork.SaveChangesAsync();

            return applicationDto;
        }

        return null;
    }

    public async Task<ApplicationVacancyDto> GetApplicationByIdAsync(string applicationId)
    {
        var applicationEntity = await _unitOfWork.ApplicationVacancyRepository.GetByIdAsync(applicationId);
        var applicationVacancyDto = _mapper.Map<ApplicationVacancyDto>(applicationEntity);

        return applicationVacancyDto;
    }

    public async Task<ApplicationVacancyDto> UpdateApplicationAsync(ApplicationVacancyDto applicationDto, string applicationId)
    {
        var existingApplicationEntity = await _unitOfWork.ApplicationVacancyRepository.GetByIdAsync(applicationId);

        if (existingApplicationEntity != null)
        {
            _mapper.Map(applicationDto, existingApplicationEntity);
            existingApplicationEntity.UpdatedAt = DateTime.Now;

            await _unitOfWork.ApplicationVacancyRepository.UpdateAsync(existingApplicationEntity);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<ApplicationVacancyDto>(existingApplicationEntity);
        }

        return null;
    }


    public async Task WithdrawApplicationAsync(string applicationId)
    {
        //Need Handle case when the application doesn't exist or other business logic.
        var applicationEntity = await _unitOfWork.ApplicationVacancyRepository.GetByIdAsync(applicationId);
        if (applicationEntity != null)
        {
            applicationEntity.UpdatedAt = DateTime.Now;

            _unitOfWork.ApplicationVacancyRepository.Remove(applicationEntity);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}

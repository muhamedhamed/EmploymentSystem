using System;
using System.Collections.Generic;
using System.Linq;
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

    public ApplicationVacancyService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public ApplicationVacancyDto? ApplyForVacancy(ApplicationVacancyDto applicationDto)
    {
        var applicationEntity = _mapper.Map<ApplicationVacancy>(applicationDto);

        //That part of the code need to be moved to another method
        var vacancyEntity = _unitOfWork.VacancyRepository.GetById(applicationEntity.VacancyId);

        var applicationsByVacancyEntity = _unitOfWork.ApplicationVacancyRepository.GetApplicationsByVacancy(applicationEntity.VacancyId);

        int numberOfApplication = applicationsByVacancyEntity.Count();

        if (applicationsByVacancyEntity != null && numberOfApplication < vacancyEntity.MaxApplications)
        {
            applicationEntity.CreatedAt = DateTime.Now;
            applicationEntity.UpdatedAt = DateTime.Now;
            _unitOfWork.ApplicationVacancyRepository.Add(applicationEntity);
            _unitOfWork.SaveChanges();
            return applicationDto;
        }
        // Default return statement
        return null;
    }
    public ApplicationVacancyDto GetApplicationById(string applicationId)
    {
        var applicationEntity = _unitOfWork.ApplicationVacancyRepository.GetById(applicationId);
        return _mapper.Map<ApplicationVacancyDto>(applicationEntity);
    }

    public ApplicationVacancyDto UpdateApplication(ApplicationVacancyDto applicationDto, string applicationId)
    {
        var existingApplicationEntity = _unitOfWork.ApplicationVacancyRepository.GetById(applicationId);
        _mapper.Map(applicationDto, existingApplicationEntity);
        existingApplicationEntity.UpdatedAt = DateTime.Now;
        _unitOfWork.ApplicationVacancyRepository.Update(existingApplicationEntity);
        _unitOfWork.SaveChanges();
        return _mapper.Map<ApplicationVacancyDto>(existingApplicationEntity);
        // Handle case when the application doesn't exist or other business logic.
    }


    public void WithdrawApplication(string applicationId)
    {
        //Need Handle case when the application doesn't exist or other business logic.
        var applicationEntity = _unitOfWork.ApplicationVacancyRepository.GetById(applicationId);
        if (applicationEntity != null)
        {
            applicationEntity.UpdatedAt = DateTime.Now;
            _unitOfWork.ApplicationVacancyRepository.Remove(applicationEntity);
            _unitOfWork.SaveChanges();
        }
    }
}

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

    public void ApplyForVacancy(ApplicationVacancyDto applicationDto)
    {
        var applicationEntity = _mapper.Map<ApplicationVacancy>(applicationDto);
        _unitOfWork.ApplicationVacancyRepository.Add(applicationEntity);
        _unitOfWork.SaveChanges();
    }

    public ApplicationVacancyDto GetApplicationById(int applicationId)
    {
        var applicationEntity = _unitOfWork.ApplicationVacancyRepository.GetById(applicationId);
        return _mapper.Map<ApplicationVacancyDto>(applicationEntity);
    }

    public void UpdateApplication(ApplicationVacancyDto applicationDto)
    {
        var existingApplicationEntity = _unitOfWork.ApplicationVacancyRepository.GetById(applicationDto.ApplicationId);
        if (existingApplicationEntity != null)
        {
            _mapper.Map(applicationDto, existingApplicationEntity);
            _unitOfWork.ApplicationVacancyRepository.Update(existingApplicationEntity);
            _unitOfWork.SaveChanges();
        }
        // Handle case when the application doesn't exist or other business logic.
    }

    public void WithdrawApplication(int applicationId)
    {
        var applicationEntity = _unitOfWork.ApplicationVacancyRepository.GetById(applicationId);
        if (applicationEntity != null)
        {
            _unitOfWork.ApplicationVacancyRepository.Remove(applicationEntity);
            _unitOfWork.SaveChanges();
        }
        // Handle case when the application doesn't exist or other business logic.
    }
    // public IEnumerable<ApplicationVacancyDto> GetApplicationsByVacancy(int vacancyId)
    // {

    // }

    // public IEnumerable<ApplicationVacancyDto> GetApplicationsByUser(int userId)
    // {

    // }
}

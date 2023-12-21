﻿using System;
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

    public ApplicationVacancyDto ApplyForVacancy(ApplicationVacancyDto applicationDto)
    {
        var applicationEntity = _mapper.Map<ApplicationVacancy>(applicationDto);
        _unitOfWork.ApplicationVacancyRepository.Add(applicationEntity);
        _unitOfWork.SaveChanges();
        return applicationDto;
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
        _unitOfWork.ApplicationVacancyRepository.Update(existingApplicationEntity);
        _unitOfWork.SaveChanges();
        return _mapper.Map<ApplicationVacancyDto>(existingApplicationEntity);
        // Handle case when the application doesn't exist or other business logic.
    }


    public void WithdrawApplication(string applicationId)
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
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

namespace App.Application;

public class VacancyService : IVacancyService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public VacancyService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public VacancyDto GetVacancyById(string vacancyId)
    {
        var vacancyEntity = _unitOfWork.VacancyRepository.GetById(vacancyId);
        return _mapper.Map<VacancyDto>(vacancyEntity);
    }

    public IEnumerable<VacancyDto> GetAllVacancies()
    {
        var vacanciesEntities = _unitOfWork.VacancyRepository.GetAll();
        return _mapper.Map<IEnumerable<VacancyDto>>(vacanciesEntities);
    }

    public VacancyDto CreateVacancy(VacancyDto vacancyDto)
    {
        var vacancyEntity = _mapper.Map<Vacancy>(vacancyDto);
        _unitOfWork.VacancyRepository.Add(vacancyEntity);
        _unitOfWork.SaveChanges();
        return vacancyDto;
    }

    public VacancyDto UpdateVacancy(VacancyDto vacancyDto, string vacancyId)
    {
        var existingVacancyEntity = _unitOfWork.VacancyRepository.GetById(vacancyId);

        _mapper.Map(vacancyDto, existingVacancyEntity);
        _unitOfWork.VacancyRepository.Update(existingVacancyEntity);
        _unitOfWork.SaveChanges();

        return _mapper.Map<VacancyDto>(existingVacancyEntity);
    }

    public void DeleteVacancy(string vacancyId)
    {
        var vacancyEntity = _unitOfWork.VacancyRepository.GetById(vacancyId);
        if (vacancyEntity != null)
        {
            _unitOfWork.VacancyRepository.Remove(vacancyEntity);
            _unitOfWork.SaveChanges();
        }
        // Handle case when the vacancy doesn't exist or other business logic.
    }

    // public IEnumerable<Vacancy> GetActiveVacancies()
    // {
    //     throw new NotImplementedException();
    // }

    // public IEnumerable<Vacancy> GetVacanciesByEmployer(int employerId)
    // {
    //     throw new NotImplementedException();
    // }
}

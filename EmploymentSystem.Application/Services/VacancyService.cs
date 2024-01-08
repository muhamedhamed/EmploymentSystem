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

    public async  Task<VacancyDto> GetVacancyByIdAsync(string vacancyId)
    {
        var vacancyEntity =await  _unitOfWork.VacancyRepository.GetByIdAsync(vacancyId);
        var vacancyDto = _mapper.Map<VacancyDto>(vacancyEntity);
        return vacancyDto;
    }

    public async Task<IEnumerable<VacancyDto>> GetAllVacanciesAsync()
    {
        var vacanciesEntities = await _unitOfWork.VacancyRepository.GetAllAsync();
        var vacanciesDto = _mapper.Map<IEnumerable<VacancyDto>>(vacanciesEntities);
        return vacanciesDto;
    }

    public async Task<VacancyDto> CreateVacancyAsync(VacancyDto vacancyDto)
    {
        var vacancyEntity = _mapper.Map<Vacancy>(vacancyDto);
        vacancyEntity.CreatedAt = DateTime.Now;
        vacancyEntity.UpdatedAt = DateTime.Now;

        await _unitOfWork.VacancyRepository.AddAsync(vacancyEntity);
        await _unitOfWork.SaveChangesAsync();

        return vacancyDto;
    }

    public async Task<VacancyDto> UpdateVacancyAsync(VacancyDto vacancyDto, string vacancyId)
    {
        var existingVacancyEntity =await  _unitOfWork.VacancyRepository.GetByIdAsync(vacancyId);

        _mapper.Map(vacancyDto, existingVacancyEntity);

        existingVacancyEntity.UpdatedAt = DateTime.Now;

        await _unitOfWork.VacancyRepository.UpdateAsync(existingVacancyEntity);
        await _unitOfWork.SaveChangesAsync();

        var updatedVacancy = _mapper.Map<VacancyDto>(existingVacancyEntity);

        return updatedVacancy;
    }

    public async Task DeleteVacancyAsync(string vacancyId)
    {
        var vacancyEntity =await _unitOfWork.VacancyRepository.GetByIdAsync(vacancyId);
        if (vacancyEntity != null)
        {
            await _unitOfWork.VacancyRepository.RemoveAsync(vacancyEntity);
            await _unitOfWork.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<ApplicationVacancyDto>> GetApplicationsByVacancyAsync(string vacancyId)
    {
        var applicationsByVacancyList =await _unitOfWork.VacancyRepository
                                .GetApplicationsByVacancyAsync(vacancyId);

        var applicationsList = _mapper.Map<IEnumerable<ApplicationVacancyDto>>(applicationsByVacancyList);
        return applicationsList;
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

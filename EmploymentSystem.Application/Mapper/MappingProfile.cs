using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Application.Mapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDto>()
            // .ForMember(R => R.RoleStr ,D =>D.MapFrom(D => D.Role.ToString()))
            .ReverseMap();

            CreateMap<Vacancy, VacancyDto>().ReverseMap();

            CreateMap<ApplicationVacancy, ApplicationVacancyDto>().ReverseMap();

            // Add other mappings as needed
        }
    }
}

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

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public UserDto GetUserById(string userId)
    {
        var userEntity = _unitOfWork.UserRepository.GetById(userId);
        return _mapper.Map<UserDto>(userEntity);
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var usersEntities = _unitOfWork.UserRepository.GetAll();
        return _mapper.Map<IEnumerable<UserDto>>(usersEntities);
    }
    public UserDto AddUser(UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        _unitOfWork.UserRepository.Add(userEntity);
        _unitOfWork.SaveChanges();
        return userDto;
    }

    public UserDto UpdateUser(UserDto userDto, string userId)
    {
        // Ad more validation
        var existingUserEntity = _unitOfWork.UserRepository.GetById(userId);
        _mapper.Map(userDto, existingUserEntity);
        _unitOfWork.UserRepository.Update(existingUserEntity);
        _unitOfWork.SaveChanges();

        return _mapper.Map<UserDto>(existingUserEntity);
        // Handle case when the user doesn't exist or other business logic.
    }

    public void DeleteUser(string userId)
    {
        var userEntity = _unitOfWork.UserRepository.GetById(userId);
        if (userEntity != null)
        {
            _unitOfWork.UserRepository.Remove(userEntity);
            _unitOfWork.SaveChanges();
        }
    }

    // public UserDto GetUserByUsername(string username)
    // {

    // }

    // public IEnumerable<UserDto> GetUsersByRole(UserRole role)
    // {

    // }
}

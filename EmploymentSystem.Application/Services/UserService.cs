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

public class UserService : IUserService
{
    private readonly IUnitOfWork<User> _unitOfWork;
      private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUnitOfWork<User> unitOfWork, IMapper mapper,IUserRepository userRepository)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
    }

    public UserDto GetUserById(string userId)
    {
        var userEntity = _unitOfWork.Repository.GetById(userId);
        return _mapper.Map<UserDto>(userEntity);
    }

    public IEnumerable<UserDto> GetAllUsers()
    {
        var usersEntities = _unitOfWork.Repository.GetAll();
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
        var existingUserEntity = _unitOfWork.Repository.GetById(userId);
        _mapper.Map(userDto, existingUserEntity);
        _unitOfWork.UserRepository.Update(existingUserEntity);
        _unitOfWork.SaveChanges();

        return _mapper.Map<UserDto>(existingUserEntity);
        // Handle case when the user doesn't exist or other business logic.
    }

    public void DeleteUser(string userId)
    {
        var userEntity = _unitOfWork.Repository.GetById(userId);
        if (userEntity != null)
        {
            _unitOfWork.Repository.Remove(userEntity);
            _unitOfWork.SaveChanges();
        }
    }

    public UserDto GetUserByEmailAndPassword(string email, string password)
    {
        var userEntity = _unitOfWork.UserRepository.GetUserByEmailAndPassword(email,password);
        // var userEntity = _userRepository.GetUserByEmailAndPassword(email,password);
        return _mapper.Map<UserDto>(userEntity);
    }
}


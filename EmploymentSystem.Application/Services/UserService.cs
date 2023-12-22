using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using System.Data.Common;


namespace EmploymentSystem.Application.Services;

public class UserService : IUserService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IConfiguration _configuration;

    public UserService(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IUserRepository userRepository,
        IConfiguration configuration
        )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userRepository = userRepository;
        _configuration = configuration;
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



    public AuthResult Authenticate(string email, string password)
    {
        // Your authentication logic here
        // If authentication is successful, generate a JWT token
        // You can use libraries like System.IdentityModel.Tokens.Jwt for token generation
        string userId;
        var user = GetUserByEmailAndPassword(email, password, out userId);

        var token = GenerateJwtToken(userId, email, password, user.Role);

        return new AuthResult { Success = true, Token = token };
    }

    public UserDto GetUserByEmailAndPassword(string email, string password, out string userId)
    {
        var userEntity = _unitOfWork.UserRepository.GetUserByEmailAndPassword(email, password);
        userId = userEntity.UserId;
        return _mapper.Map<UserDto>(userEntity);
    }

    private string GenerateJwtToken(string userId, string email, string password, string role)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Authentication:SecretForKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("sub", userId),
            new Claim("userId", userId),
            new Claim("email", email),
            new Claim("password", password),
            new Claim("role", role),
            // You can add additional claims here
        };

        var token = new JwtSecurityToken(
            issuer: _configuration["Authentication:Issuer"],
            audience: _configuration["Authentication:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Authentication:ExpiresInMinutes"])),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}



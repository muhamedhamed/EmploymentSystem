using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using AutoMapper;
using EmploymentSystem.Application.Dtos;
using EmploymentSystem.Application.Interfaces;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.Extensions.Configuration;


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

    public async Task<UserDto> GetUserByIdAsync(string userId)
    {
        var userEntity = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        return  _mapper.Map<UserDto>(userEntity);
    }

    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        var usersEntities =await  _unitOfWork.UserRepository.GetAllAsync();
        return _mapper.Map<IEnumerable<UserDto>>(usersEntities);
    }
    
    public async Task<UserDto> AddUserAsync(UserDto userDto)
    {
        var userEntity = _mapper.Map<User>(userDto);
        userEntity.CreatedAt = DateTime.Now;
        userEntity.UpdatedAt = DateTime.Now;

        await _unitOfWork.UserRepository.AddAsync(userEntity);
        await _unitOfWork.SaveChangesAsync();
        return userDto;
    }

    public async Task<UserDto> UpdateUserAsync(UserDto userDto, string userId)
    {
        // Ad more validation
        var existingUserEntity =await _unitOfWork.UserRepository.GetByIdAsync(userId);
        _mapper.Map(userDto, existingUserEntity);
        existingUserEntity.UpdatedAt = DateTime.Now;
        await _unitOfWork.UserRepository.UpdateAsync(existingUserEntity);
        await _unitOfWork.SaveChangesAsync();

        return _mapper.Map<UserDto>(existingUserEntity);
    }

    public async Task DeleteUserAsync(string userId)
    {
        var userEntity = await _unitOfWork.UserRepository.GetByIdAsync(userId);
        if (userEntity != null)
        {
            _unitOfWork.UserRepository.Remove(userEntity);
            await _unitOfWork.SaveChangesAsync();
        }
    }



    public async Task<AuthResult> AuthenticateAsync(string email, string password)
    {
        var (user, userId) =await GetUserByEmailAndPasswordAsync(email, password);

        var token = GenerateJwtToken(userId, email, password, user.Role);

        return new AuthResult { Success = true, Token = token };
    }

    // in the future I will remove the user id from userDto
    // search for best practise for the DTO and userId and Password
    public async Task<(UserDto user,string userId)> GetUserByEmailAndPasswordAsync(string email, string password)
    {
        var userEntity =await _unitOfWork.UserRepository.GetUserByEmailAndPasswordAsync(email, password);
        var userId = userEntity.UserId;
        var userDto = _mapper.Map<UserDto>(userEntity);
        return (userDto, userId) ;
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



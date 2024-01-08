using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IUserService
{
    Task<UserDto> GetUserByIdAsync(string userId);
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto> AddUserAsync(UserDto userDto);
    Task<UserDto> UpdateUserAsync(UserDto userDto, string userId);
    Task DeleteUserAsync(string userId);
    Task<(UserDto user, string userId)> GetUserByEmailAndPasswordAsync(string email, string password);
    Task<AuthResult> AuthenticateAsync(string email, string password);
    // Task<UserDto> GetUserByUsernameAsync(string username);
    // Task<IEnumerable<UserDto>> GetUsersByRoleAsync(UserDto role);
}

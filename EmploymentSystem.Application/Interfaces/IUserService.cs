using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IUserService
{
    UserDto GetUserById(int userId);
    IEnumerable<UserDto> GetAllUsers();
    void AddUser(UserDto userDto);
    void UpdateUser(UserDto userDto);
    void DeleteUser(int userId);
    // UserDto GetUserByUsername(string username);
    // IEnumerable<UserDto> GetUsersByRole(UserDto role);
    // // Other methods related to user management...
}

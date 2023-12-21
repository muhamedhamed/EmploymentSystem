using EmploymentSystem.Application.Dtos;

namespace EmploymentSystem.Application.Interfaces;

public interface IUserService
{
    UserDto GetUserById(string userId);
    IEnumerable<UserDto> GetAllUsers();
    UserDto AddUser(UserDto userDto);
    UserDto UpdateUser(UserDto userDto,string userId);
    void DeleteUser(string userId);
    UserDto GetUserByEmailAndPassword(string email, string password);

    // UserDto GetUserByUsername(string username);
    // IEnumerable<UserDto> GetUsersByRole(UserDto role);
    // // Other methods related to user management...
}

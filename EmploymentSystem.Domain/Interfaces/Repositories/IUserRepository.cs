using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories;

public interface IUserRepository : IGenericRepository<User>
{
     // User GetUserByUsername(string username);
     // IEnumerable<User> GetUsersByRole(UserRole role);
}

using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories;

public interface IUserRepository: IGenericRepository<User>
{
     User GetUserByEmailAndPassword(string email, string password);
     // IEnumerable<User> GetUsersByRole(UserRole role);
}

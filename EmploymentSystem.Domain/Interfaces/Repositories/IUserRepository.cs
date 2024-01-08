using EmploymentSystem.Domain.Entities;

namespace EmploymentSystem.Domain.Interfaces.Repositories;

public interface IUserRepository: IGenericRepository<User>
{
     Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
}

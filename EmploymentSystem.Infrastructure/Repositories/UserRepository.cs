using System.Runtime.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Entities;
using EmploymentSystem.Domain.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EmploymentSystem.Infrastructure.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context) :base(context)
        {
            _context = context;
        }
        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            // Find the user by email and password
            // Handle null return
            var user =await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password);

            return user;
        }
    }
}

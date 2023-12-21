using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Common;

namespace EmploymentSystem.Domain.Entities
{
    public class User:Base
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Username { get; set; } // currently I will consider the email and username the same
        public string Password { get; set; } // first we will add it as string then we can add hashing
        public UserRole Role { get; set; } // Enum to represent user roles

        // Navigation properties
        public ICollection<Vacancy> CreatedVacancies { get; set; }
        public ICollection<ApplicationVacancy> Applications { get; set; }
    }

    public enum UserRole
    {
        Employer,
        Applicant
    }
}

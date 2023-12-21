using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Common;

namespace EmploymentSystem.Domain.Entities
{
    public class Vacancy:Base
    {
        public string VacancyId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Salary { get; set; }
        public string Location { get; set; }
        public int MaxApplications { get; set; }
        public DateTime ExpiryDate { get; set; }
        public bool IsExpired { get; set; } // Check the vacancy existance or not

        // Navigation properties
        public string EmployerId { get; set; } // Foreign key
        public User Employer { get; set; } // Navigation property

        public ICollection<ApplicationVacancy> Applications { get; set; }
    }
}

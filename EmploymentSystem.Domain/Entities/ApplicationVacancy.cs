using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmploymentSystem.Domain.Common;

namespace EmploymentSystem.Domain.Entities
{
    public class ApplicationVacancy:Base
    {
        public string ApplicationVacancyId { get; set; }

        // Navigation properties
        public string ApplicantId { get; set; } // Foreign key
        public User Applicant { get; set; } // Navigation property
        public string VacancyId { get; set; } // Foreign key
        public Vacancy AppliedVacancy { get; set; } // Navigation property
    }
}

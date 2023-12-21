using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmploymentSystem.Domain.Common
{
    public class Base
    {
        public DateTime CreatedAt { get; set; } // Timestamp for creation
        public DateTime UpdatedAt { get; set; } // Timestamp for last update
        public bool IsActive { get; set; } // Soft delete flag
    }
}

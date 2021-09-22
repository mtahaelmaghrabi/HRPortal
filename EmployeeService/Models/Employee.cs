using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.Models
{
    public class Employee
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string ProfilePicture { get; set; }

        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }

        public string TeamID { get; set; } // Assigned Team ID (Workflow team)

        public List<EmployeeAddress> EmployeeAddress { get; set; } = new List<EmployeeAddress>();
        public bool IsDeleted { get; internal set; }
    }
}
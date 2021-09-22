using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.Dtos
{
    public class EmployeeCreateDto
    {
        [Required]
        public string EmployeeCode { get; set; }
        [Required]
        public string EmployeeName { get; set; }
        public string ProfilePicture { get; set; }

        public string Mobile { get; set; }
        public bool Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public DateTime HiringDate { get; set; }
        public string Email { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequestService.Dtos
{
    public class EmployeePublishDto
    {
        public Guid EmployeeID { get; set; }
        public Guid ExternalEmployeeID { get; set; } // Just to test custome mapping using AutoMapper
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EmploymentStatusID { get; set; }

        public string Event { get; set; }
    }
}
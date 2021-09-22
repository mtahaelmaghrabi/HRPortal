using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequestService.Models
{
    public class LeaveRequest
    {
        [Required]
        public int LeaveRequestID { get; set; }
        [Required]
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public double BalanceBefore { get; set; }
        public double BalanceAfter { get; set; }
        
        //TODO: To be added
        //public int RequestYear { get; set; }
        //public int RequestMonth { get; set; }
        public string Status { get; set; } // Open - Approved - Rejected
        public string Notes { get; set; }

        public Guid EmployeeID { get; set; }
        public Employee employee { get; set; }

    }
}
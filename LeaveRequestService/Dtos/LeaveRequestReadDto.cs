using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequestService.Dtos
{
    public class LeaveRequestReadDto
    {
        public int LeaveRequestID { get; set; }
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NumberOfDays { get; set; }
        public double BalanceBefore { get; set; }
        public double BalanceAfter { get; set; }
        public string Notes { get; set; }
    }
}
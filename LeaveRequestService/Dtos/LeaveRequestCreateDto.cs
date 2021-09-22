using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequestService.Dtos
{
    public class LeaveRequestCreateDto
    {
        [Required]
        public int LeaveTypeID { get; set; }
        public string LeaveTypeName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
        public string Notes { get; set; }
    }
}

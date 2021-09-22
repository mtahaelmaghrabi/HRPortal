using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace EmployeeService.Models
{
    public class EmployeeAddress
    {
        public Guid RecordID { get; set; }

        public string ContactName { get; set; }

        public string AddressLine1 { get; set; }

        public string AddressLine2 { get; set; }

        public string City { get; set; }

        public string CountryID { get; set; }

        public string AddressType { get; set; } // WORK - HOME - VACATION

        public DateTime RecordStartDate { get; set; }
        public DateTime RecordEndtDate { get; set; }
        public bool IsCurrentRecord { get; set; }

        public Guid EmployeeID { get; set; }

        [ForeignKey(nameof(EmployeeID))]
        public Employee Employee { get; set; }
        public bool IsDeleted { get; internal set; }
    }
}
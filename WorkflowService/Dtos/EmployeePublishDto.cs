using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WorkflowService.AzureMessaging.Dtos
{
    public class EmployeePublishDto : IntegrationBaseMessageDto
    {
        public Guid EmployeeID { get; set; }
        public string EmployeeCode { get; set; }
        public string EmployeeName { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string EmploymentStatusID { get; set; }

        public string Event { get; set; }
    }
}
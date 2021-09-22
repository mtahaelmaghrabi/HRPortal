using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowService.AzureMessaging.Dtos;

namespace WorkflowService.AzureMessaging
{
    public class ProcessData : IProcessData
    {
        private IConfiguration _configuration;

        public ProcessData(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task Process(EmployeePublishDto myPayload)
        {
            Console.WriteLine($"Employee: [{myPayload.EmployeeCode}] & ID:[{myPayload.EmployeeID}] has been received fro Azure Service Bus");
        }
    }
}
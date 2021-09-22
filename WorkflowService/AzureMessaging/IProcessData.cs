using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorkflowService.AzureMessaging.Dtos;

namespace WorkflowService.AzureMessaging
{
    public interface IProcessData
    {
        Task Process(EmployeePublishDto myPayload);
    }
}

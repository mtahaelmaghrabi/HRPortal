using EmployeeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.SyncDataServices.Http
{
    public interface ILeaveRequestDataClient
    {
        Task SendEmployeeToLeaveRequest(EmployeeReadDto employee);
    }
}

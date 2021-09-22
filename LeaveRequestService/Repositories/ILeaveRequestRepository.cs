using LeaveRequestService.Models;
using System;
using System.Collections.Generic;

namespace EmployeeService.Repositories
{
    public interface ILeaveRequestRepository
    {
        bool SaveChanges();

        // Employee Endpoints:
        IEnumerable<Employee> GetAllEmployees();
        bool EmployeeExist(Guid employeeID);
        void CreateEmployee(Employee employee);


        // Leave Request Endpoints:
        IEnumerable<LeaveRequest> GetEmployeeLeaveRequests(Guid employeeID);
        IEnumerable<LeaveRequest> GetOpenLeaveRequestsByStatus(Guid employeeID, string statusCode);

        LeaveRequest GetLeaveRequest(Guid employeeID, int id);
        void CreateLeaveRequest(Guid employeeID, LeaveRequest request);
    }
}
using LeaveRequestService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Repositories
{
    public class LeaveRequestRepository : ILeaveRequestRepository
    {
        private readonly LeaveRequestDbContext _context;
        public LeaveRequestRepository(LeaveRequestDbContext context)
        {
            this._context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        // Employee Endpoints:
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _context.Employee.ToList();
        }
        public bool EmployeeExist(Guid employeeID)
        {
            return _context.Employee.Any(e => e.EmployeeID == employeeID);
        }
        public void CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            // We are not generating any new ID, as it is coming from Employee Service as it is
            //employee.EmployeeID = Guid.NewGuid();

            _context.Employee.Add(employee);

            _context.SaveChanges();
        }


        // Leave Request Endpoints:
        public IEnumerable<LeaveRequest> GetEmployeeLeaveRequests(Guid employeeID)
        {
            return _context.LeaveRequest.Where(req => req.EmployeeID == employeeID).ToList();
        }
        public IEnumerable<LeaveRequest> GetOpenLeaveRequestsByStatus(Guid employeeID, string statusCode)
        {
            return _context.LeaveRequest.Where(req => req.EmployeeID == employeeID && req.Status == statusCode).ToList();
        }
        public LeaveRequest GetLeaveRequest(Guid employeeID, int requestID)
        {
            return _context.LeaveRequest.FirstOrDefault(req => req.EmployeeID == employeeID && req.LeaveRequestID == requestID);
        }
        public void CreateLeaveRequest(Guid employeeID, LeaveRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // TODO: to be calculated after
            // Not to be as aparameters from the API end point.
            request.NumberOfDays = Convert.ToInt32((request.EndDate - request.StartDate).TotalDays);
            request.BalanceBefore = 145; // Get the current balance
            request.BalanceAfter = request.BalanceBefore - (double)request.NumberOfDays;

            //request.LeaveRequestID = 10001;
            request.EmployeeID = employeeID;

            _context.LeaveRequest.Add(request);

            _context.SaveChanges();
        }
    }
}
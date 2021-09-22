using EmployeeService.Models;
using System;
using System.Collections.Generic;

namespace EmployeeService.Repositories
{
    public interface IEmployeeRepository
    {
        bool SaveChanges();

        IEnumerable<Employee> GetAllEmployee();

        Employee GetEmployeeById(Guid id);

        Employee CreateEmployee(Employee employee);

        Employee UpdateEmployee(Employee employee);

        Employee DeleteEmployee(Guid id);
    }
}
using EmployeeService.Models;
using System;
using System.Collections.Generic;

namespace EmployeeService.Repositories
{
    public interface IAddressRepository
    {
        bool SaveChanges();

        IEnumerable<EmployeeAddress> GetEmployeeAddresses(Guid employeeID);

        EmployeeAddress GetEmployeeAddress(Guid employeeID, Guid addressID);

        EmployeeAddress CreateAddress(Guid employeeID, EmployeeAddress employeeAddress);

        EmployeeAddress UpdateAddress(EmployeeAddress employeeAddress);

        EmployeeAddress DeleteAddress(Guid employeeID, Guid addressID);

        EmployeeAddress GetCurrentAddress(Guid employeeID);

    }
}
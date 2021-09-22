using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.Repositories
{
    public class AddressRepository : IAddressRepository
    {
        private readonly EmployeeDbContext _context;
        public AddressRepository(EmployeeDbContext context)
        {
            this._context = context;
        }
        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }


        public EmployeeAddress CreateAddress(Guid employeeID, EmployeeAddress employeeAddress)
        {
            if (employeeAddress == null)
            {
                throw new ArgumentNullException(nameof(employeeAddress));
            }

            employeeAddress.EmployeeID = employeeID;
            employeeAddress.RecordID = Guid.NewGuid();

            _context.EmployeeAddress.Add(employeeAddress);

            _context.SaveChanges();

            return employeeAddress;
        }

        public EmployeeAddress DeleteAddress(Guid employeeID, Guid addressID)
        {
            EmployeeAddress address = GetEmployeeAddress(employeeID, addressID);

            if (address == null)
            {
                throw new ArgumentNullException(nameof(address));
            }

            // For Soft delete > Never delete phisically.
            address.IsDeleted = true;

            _context.EmployeeAddress.Update(address);

            _context.SaveChanges();

            return address;
        }

        public EmployeeAddress GetEmployeeAddress(Guid employeeID, Guid addressID)
        {
            return _context.EmployeeAddress.FirstOrDefault(e => e.EmployeeID == employeeID && e.RecordID == addressID);
        }

        public IEnumerable<EmployeeAddress> GetEmployeeAddresses(Guid employeeID)
        {
            throw new NotImplementedException();
        }

        public EmployeeAddress UpdateAddress(EmployeeAddress employeeAddress)
        {
            if (employeeAddress == null)
            {
                throw new ArgumentNullException(nameof(employeeAddress));
            }

            _context.EmployeeAddress.Update(employeeAddress);

            _context.SaveChanges();

            return employeeAddress;
        }
    }
}

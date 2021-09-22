using EmployeeService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeService.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly EmployeeDbContext _context;
        public EmployeeRepository(EmployeeDbContext context)
        {
            this._context = context;
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }

        public Employee CreateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            employee.EmployeeID = Guid.NewGuid();

            _context.Employee.Add(employee);

            _context.SaveChanges();

            return employee;
        }

        public Employee UpdateEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            _context.Employee.Update(employee);

            _context.SaveChanges();

            return employee;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            return _context.Employee.ToList();
        }

        public Employee GetEmployeeById(Guid id)
        {
            return _context.Employee.FirstOrDefault(e => e.EmployeeID == id);
        }

        public Employee DeleteEmployee(Guid id)
        {
            Employee employee = GetEmployeeById(id);

            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            // For Soft delete > Never delete phisically.
            employee.IsDeleted = true;

            _context.Employee.Update(employee);

            _context.SaveChanges();

            return employee;
        }
    }
}
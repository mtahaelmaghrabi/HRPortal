using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeaveRequestService.Configurations;
using LeaveRequestService.Models;
using Microsoft.EntityFrameworkCore;

namespace EmployeeService
{
    public class LeaveRequestDbContext : DbContext
    {
        public DbSet<Employee> Employee { get; set; }
        public DbSet<LeaveRequest> LeaveRequest { get; set; }

        public LeaveRequestDbContext(DbContextOptions<LeaveRequestDbContext> opt) : base(opt)
        {

        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new EmployeeConfiguration());
            builder.ApplyConfiguration(new LeaveRequestConfiguration());
        }
    }
}
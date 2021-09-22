using LeaveRequestService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveRequestService.Configurations
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder
                .HasKey(a => a.EmployeeID);

            builder
                .Property(m => m.EmployeeName)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .ToTable("Employee");

            // Define the relationship between our tow models (Employee & LeaveRequests)
            builder
                .HasMany(l => l.LeaveRequests)
                .WithOne(e => e.employee!)
                .HasForeignKey(e => e.EmployeeID);

        }
    }
}
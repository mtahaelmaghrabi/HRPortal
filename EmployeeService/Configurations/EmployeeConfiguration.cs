using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Configurations
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
        }
    }
}
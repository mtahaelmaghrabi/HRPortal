using EmployeeService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EmployeeService.Configurations
{
    public class EmployeeAddressConfiguration : IEntityTypeConfiguration<EmployeeAddress>
    {
        public void Configure(EntityTypeBuilder<EmployeeAddress> builder)
        {
            builder
                .HasKey(a => a.RecordID);

            builder
                .Property(m => m.ContactName)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .ToTable("EmployeeAddress");
        }
    }
}
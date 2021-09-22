using LeaveRequestService.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LeaveRequestService.Configurations
{
    public class LeaveRequestConfiguration : IEntityTypeConfiguration<LeaveRequest>
    {
        public void Configure(EntityTypeBuilder<LeaveRequest> builder)
        {
            builder
                .HasKey(a => a.LeaveRequestID);

            builder
                .Property(m => m.LeaveTypeName)
                .IsRequired()
                .HasMaxLength(300);

            builder
                .ToTable("LeaveRequest");

            // Define the relationship between our tow models (Employee & LeaveRequests)
            builder
                .HasOne(l => l.employee)
                .WithMany(e => e.LeaveRequests)
                .HasForeignKey(e => e.EmployeeID);
        }
    }
}
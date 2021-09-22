using LeaveRequestService.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.Data
{
    public class PrepDB
    {
        public static void CreateInitialDatabase(IApplicationBuilder app, bool isProduction)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<LeaveRequestDbContext>(), isProduction);
            }
        }

        public static void SeedData(LeaveRequestDbContext context, bool isProduction)
        {
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            // NO NEED  if (isProduction)
            if (true)
            {
                Console.WriteLine(" ..... Start applying Migration!");

                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Migration Error: {ex.Message}");
                }
            }

            if (context.LeaveRequest.Any())
            {
                Console.WriteLine(" We Already have some data");

                return;
            }

            Console.WriteLine(" ..... Seeding data");


            // Create the initial Data
            Guid fixedEmployeeID = Guid.Parse("d9742d91-dc17-4be7-a715-55cc9d231c55");
            //int id = 101;
            Employee emp = new Employee()
            {
                EmployeeID = fixedEmployeeID,
                EmployeeName = "Mohamed Taha",
                HiringDate = new DateTime(2015, 3, 15),
                Mobile = "66130236000",
                Email = "mtaha@pp.gov.qa"
            };


            LeaveRequest req = new LeaveRequest()
            {
                EmployeeID = fixedEmployeeID,
                //LeaveRequestID = 101,
                LeaveTypeID = 7,
                LeaveTypeName = "Annual Leave",
                BalanceBefore = 45,
                BalanceAfter = 40,
                NumberOfDays = 5,
                StartDate = new DateTime(2021, 10, 1),
                EndDate = new DateTime(2021, 10, 5),
                Status = "Open",
                Notes = "Five days leave for summer vacations :)"
            };

            context.LeaveRequest.AddRange(new LeaveRequest()
            {
                EmployeeID = fixedEmployeeID,
                LeaveTypeID = 7,
                LeaveTypeName = "Annual Leave",
                BalanceBefore = 45,
                BalanceAfter = 40,
                NumberOfDays = 5,
                StartDate = new DateTime(2021, 10, 1),
                EndDate = new DateTime(2021, 10, 5),
                Status = "Open",
                Notes = "Five days leave for summer vacations :)"
            },
            new LeaveRequest()
            {
                EmployeeID = fixedEmployeeID,
                LeaveTypeID = 1,
                LeaveTypeName = "Casual Leave",
                BalanceBefore = 7,
                BalanceAfter = 6,
                NumberOfDays = 1,
                StartDate = new DateTime(2021, 9, 9),
                EndDate = new DateTime(2021, 9, 9),
                Status = "Approved",
                Notes = "Hospital appointment"
            },
            new LeaveRequest()
            {
                EmployeeID = fixedEmployeeID,
                LeaveTypeID = 1,
                LeaveTypeName = "Sick Leave",
                BalanceBefore = 99,
                BalanceAfter = 90,
                NumberOfDays = 9,
                StartDate = new DateTime(2021, 5, 1),
                EndDate = new DateTime(2021, 5, 9),
                Status = "Rejected",
                Notes = "COVID-19"
            },
            new LeaveRequest()
            {
                EmployeeID = fixedEmployeeID,
                LeaveTypeID = 1,
                LeaveTypeName = "Sick Leave",
                BalanceBefore = 99,
                BalanceAfter = 84,
                NumberOfDays = 15,
                StartDate = new DateTime(2021, 1, 1),
                EndDate = new DateTime(2021, 1, 15),
                Status = "Approved",
                Notes = "COVID-19 Quarantine"
            });

            context.Employee.Add(emp);
            //context.LeaveRequest.Add(req);


            context.SaveChanges();
        }
    }
}
using EmployeeService.Models;
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
                SeedData(serviceScope.ServiceProvider.GetService<EmployeeDbContext>(), isProduction);
            }
        }

        public static void SeedData(EmployeeDbContext context, bool isProduction)
        {
            //context.Database.EnsureDeleted();
            //context.Database.EnsureCreated();

            // NO NEED  if (isProduction)
            if(true)
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

            if (context.Employee.Any())
            {
                Console.WriteLine(" We Already have some data");

                return;
            }

            Console.WriteLine(" ..... Seeding data");


            // Create the initial Data
            Guid id = Guid.Parse("d9742d91-dc17-4be7-a715-55cc9d231c55");
            //int id = 101;
            Employee emp = new Employee()
            {
                EmployeeID = id,
                EmployeeName = "Mohamed Taha",
                Birthdate = new DateTime(1982, 6, 1),
                HiringDate = new DateTime(2015, 3, 15),
                Gender = true,
                Mobile = "66130236000",
                ProfilePicture = id.ToString() + ".jpg",
                Email = "mtaha@pp.gov.qa",
                EmployeeAddress = new List<EmployeeAddress>() {
                    new EmployeeAddress() {
                        EmployeeID = id,
                        RecordID = Guid.NewGuid(),
                        AddressLine1 = "Bin Dinar st.",
                        AddressLine2 = "Building # 11",
                        City = "Al-Sadd",
                        CountryID = "QA",
                        ContactName = "Mohamed Taha" },
                    new EmployeeAddress() {
                        EmployeeID = id,
                        RecordID = Guid.NewGuid(),
                        AddressLine1 = "Ain Khaled",
                        AddressLine2 = "Building # 7",
                        City = "Doha",
                        CountryID = "QA",
                        ContactName = "Mohamed Taha"} }
            };

            Guid id2 = Guid.Parse("d9742d91-dc17-4be7-a715-55cc9d231c90");
            //int id = 101;
            Employee emp2 = new Employee()
            {
                EmployeeID = id2,
                EmployeeName = "Ibrahim",
                Birthdate = new DateTime(1983, 4, 1),
                HiringDate = new DateTime(2016, 1, 1),
                Gender = true,
                Mobile = "33789097",
                ProfilePicture = id.ToString() + ".jpg",
                Email = "ameen@pp.gov.qa",
                EmployeeAddress = new List<EmployeeAddress>() {
                    new EmployeeAddress() {
                        EmployeeID = id,
                        RecordID = Guid.NewGuid(),
                        AddressLine1 = "Bin Dinar st.",
                        AddressLine2 = "Building # 13",
                        City = "Al-Sadd",
                        CountryID = "QA",
                        ContactName = "Ameen" },
                    new EmployeeAddress() {
                        EmployeeID = id,
                        RecordID = Guid.NewGuid(),
                        AddressLine1 = "Ain Khaled",
                        AddressLine2 = "Building # 3",
                        City = "Doha",
                        CountryID = "QA",
                        ContactName = "Ameen 2"} }
            };

            context.Employee.Add(emp);
            context.Employee.Add(emp2);

            context.SaveChanges();
        }
    }
}
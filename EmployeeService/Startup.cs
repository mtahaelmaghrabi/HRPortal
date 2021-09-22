using EmployeeService.AsyncDataServices;
using EmployeeService.AzureMessaging;
using EmployeeService.Data;
using EmployeeService.Repositories;
using EmployeeService.SyncDataServices.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public IWebHostEnvironment _env { get; }

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Configuration = configuration;
            _env = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                Console.WriteLine("--> Using SQL-Server Database");
                services.AddDbContext<EmployeeDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));
            }
            else
            {
                Console.WriteLine("--> Using In-Memory Database");
                //services.AddDbContext<EmployeeDbContext>(opt => opt.UseInMemoryDatabase("InMem"));
                services.AddDbContext<EmployeeDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("EmployeeDbConnection")));
                Console.WriteLine($"--> In-Memory Database Connection: {Configuration.GetConnectionString("EmployeeDbConnection")}");

            }

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            services.AddHttpClient<ILeaveRequestDataClient, LeaveRequestDataClient>();

            services.AddSingleton<IMessageBusClient, MessageBusClient>(); // RabbitMQ
            services.AddSingleton<IMessageBus, AzServiceBusMessaging>(); // Azure Service Bus


            services.AddControllers();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "EmployeeService", Version = "v1" });
            });

            Console.WriteLine($"--> LeaveRequest Service Endpoint {Configuration["LeaveRequestService"]}");
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "EmployeeService v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            PrepDB.CreateInitialDatabase(app, _env.IsProduction());
        }
    }
}

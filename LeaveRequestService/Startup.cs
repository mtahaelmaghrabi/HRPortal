using EmployeeService;
using EmployeeService.Data;
using EmployeeService.Repositories;
using LeaveRequestService.AsyncDataServices;
using LeaveRequestService.EventProcessing;
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

namespace LeaveRequestService
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

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            if (_env.IsProduction())
            {
                services.AddDbContext<LeaveRequestDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LeaveRequestDbConnection")));
            }
            else
            {
                services.AddDbContext<LeaveRequestDbContext>(opt => opt.UseSqlServer(Configuration.GetConnectionString("LeaveRequestDbConnection")));
            }
            services.AddScoped<ILeaveRequestRepository, LeaveRequestRepository>();
            services.AddSingleton<IEventProcessor, EventProcessor>();

            services.AddControllers();
            
            services.AddHostedService<MessageBusSubscriber>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "LeaveRequestService", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment() || env.IsProduction())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "LeaveRequestService v1"));
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
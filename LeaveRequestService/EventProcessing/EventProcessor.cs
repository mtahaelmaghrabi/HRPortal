using AutoMapper;
using EmployeeService.Repositories;
using LeaveRequestService.Dtos;
using LeaveRequestService.Models;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LeaveRequestService.EventProcessing
{
    public class EventProcessor : IEventProcessor
    {
        public IServiceScopeFactory scopeFactory { get; }
        public IMapper mapper { get; }

        public EventProcessor(IServiceScopeFactory scopeFactory, IMapper mapper)
        {
            this.scopeFactory = scopeFactory;
            this.mapper = mapper;
        }

        public void ProcessEvent(string message)
        {
            var eventType = DetermineEvent(message);
            switch (eventType)
            {
                case EventType.EmployeePublished:
                    // TODO:
                    AddEmployee(message);
                    break;
                case EventType.DayOffPublished:
                    break;
                case EventType.Undetermend:
                    break;
                default:
                    break;
            }
        }

        private EventType DetermineEvent(string notificationMessage)
        {
            Console.WriteLine("--> Determining Event");
            var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);
            switch (eventType.Event)
            {
                case "Employee_Published":
                    Console.WriteLine("--> Employee_Published event has been determined");
                    return EventType.EmployeePublished;

                default:
                    Console.WriteLine("--> Could not determine the event type");

                    return EventType.EmployeePublished;
            }
        }

        private void AddEmployee(string employeePublishedMessage)
        {
            using (var scope = scopeFactory.CreateScope())
            {
                var repo = scope.ServiceProvider.GetRequiredService<ILeaveRequestRepository>();
                var employeePublishedDto = JsonSerializer.Deserialize<EmployeePublishDto>(employeePublishedMessage);

                try
                {
                    var employee = mapper.Map<Employee>(employeePublishedDto);

                    if (repo.EmployeeExist(employee.EmployeeID))
                    {
                        Console.WriteLine($"--> Employee #: [{employee.EmployeeID}] Is already exist in the Leave-Request DB");
                    }
                    else
                    {
                        repo.CreateEmployee(employee);
                        Console.WriteLine($"--> Employee With ID: [{employee.EmployeeID}] has been added");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not add Employee_Published to the DB, Error:{ex.Message}");

                    throw;
                }
            }
        }

        enum EventType
        {
            EmployeePublished,
            DayOffPublished, // just a dummy sample
            Undetermend
        }
    }
}
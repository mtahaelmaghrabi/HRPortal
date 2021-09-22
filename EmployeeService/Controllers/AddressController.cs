using AutoMapper;
using EmployeeService.AsyncDataServices;
using EmployeeService.AzureMessaging;
using EmployeeService.Dtos;
using EmployeeService.Models;
using EmployeeService.Repositories;
using EmployeeService.SyncDataServices.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.Controllers
{
    [Route("api/address/employee/{employeeid}/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmployeeRepository employeeRepo;
        private readonly IMessageBusClient messageBusClient;
        private readonly IMessageBus messageBus;
        private readonly IConfiguration configuration;

        public AddressController(IMapper mapper,
            IEmployeeRepository employeeRepo,
            ILeaveRequestDataClient leaveRequestDataClient,
            IMessageBusClient messageBusClient,
            IMessageBus messageBus,
            IConfiguration configuration)
        {
            this.mapper = mapper;
            this.employeeRepo = employeeRepo;
            this.messageBusClient = messageBusClient;
            this.messageBus = messageBus;
            this.configuration = configuration;
        }





        // POST: AddressController/Delete/5
        //[HttpPost]
        //[ValidateAntiForgeryToken] ??   
    }
}

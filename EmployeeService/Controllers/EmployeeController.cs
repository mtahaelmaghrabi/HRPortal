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
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IEmployeeRepository employeeRepo;
        private readonly ILeaveRequestDataClient leaveRequestDataClient;
        private readonly IMessageBusClient messageBusClient;
        private readonly IMessageBus messageBus;
        private readonly IConfiguration configuration;

        public EmployeeController(IMapper mapper,
            IEmployeeRepository employeeRepo,
            ILeaveRequestDataClient leaveRequestDataClient,
            IMessageBusClient messageBusClient,
            IMessageBus messageBus,
            IConfiguration configuration)
        {
            this.mapper = mapper;
            this.employeeRepo = employeeRepo;
            this.leaveRequestDataClient = leaveRequestDataClient;
            this.messageBusClient = messageBusClient;
            this.messageBus = messageBus;
            this.configuration = configuration;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetAllEmployees()
        {
            var employees = employeeRepo.GetAllEmployee();

            if (employees == null)
                return NotFound();

            return Ok(mapper.Map<IEnumerable<EmployeeReadDto>>(employees));
        }

        [HttpGet("{id}", Name = "GetEmployeeByID")]
        public ActionResult<EmployeeReadDto> GetEmployeeByID(Guid id)
        {
            var employee = employeeRepo.GetEmployeeById(id);

            if (employee == null)
                return NotFound();

            return Ok(mapper.Map<EmployeeReadDto>(employee));
        }


        [HttpPost]
        public async Task<ActionResult<EmployeeReadDto>> CreateEmployee([FromBody] EmployeeCreateDto employee)
        {
            var employeeModel = mapper.Map<Employee>(employee);

            employeeRepo.CreateEmployee(employeeModel);
            employeeRepo.SaveChanges();


            var employeeReadDto = mapper.Map<EmployeeReadDto>(employeeModel);

            // Sending the new employee Message

            try
            {
                // Publish Employee as message using RabbitMQ
                var employeeMessage = mapper.Map<EmployeePublishDto>(employeeReadDto);

                employeeMessage.EmploymentStatusID = "Active";
                employeeMessage.Event = "Employee_Published";

                Console.WriteLine($"--> Trying to send Employee Number [{employeeMessage.EmployeeID}] to RabbitMQ");
                messageBusClient.PublishNewEmployee(employeeMessage);
                Console.WriteLine($"--> Employee Number [{employeeMessage.EmployeeID}] has been sent to RabbitMQ");

                // Publish Employee as message using RabbitMQ
                // Add "employee creation" message to the service bus topic
                employeeMessage.ID = new Guid();
                employeeMessage.CreationDateTime = DateTime.Now;

                Console.WriteLine($"--> Trying to send Employee Number [{employeeMessage.EmployeeID}] to Azure Service Bus");

                await messageBus.PublishMessage(employeeMessage, configuration["AzureServiceBusTopicName"]);

                Console.WriteLine($"--> Employee Number [{employeeMessage.EmployeeID}] has been sent to Azure Service Bus");


                //await leaveRequestDataClient.SendEmployeeToLeaveRequest(employeeReadDto);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not send syncronosouly: {ex.Message}");
            }

            return CreatedAtRoute(nameof(GetEmployeeByID), new { Id = employeeReadDto.EmployeeID }, employeeReadDto);
        }
    }
}
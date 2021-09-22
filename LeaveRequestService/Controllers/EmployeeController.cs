using AutoMapper;
using EmployeeService.Repositories;
using LeaveRequestService.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequestService.Controllers
{
    [Route("api/leave/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepo;

        public EmployeeController(IMapper mapper, ILeaveRequestRepository leaveRequestRepo)
        {
            this.mapper = mapper;
            this.leaveRequestRepo = leaveRequestRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<EmployeeReadDto>> GetAllEmployees()
        {
            Console.WriteLine("--> Getting All employees from LeaveRequest Services ...");

            var employees = leaveRequestRepo.GetAllEmployees();

            if (employees == null)
                return NotFound();

            return Ok(mapper.Map<IEnumerable<EmployeeReadDto>>(employees));
        }
    }
}
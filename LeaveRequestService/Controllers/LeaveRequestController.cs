using AutoMapper;
using EmployeeService.Repositories;
using LeaveRequestService.Dtos;
using LeaveRequestService.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaveRequestService.Controllers
{
    [Route("api/leave/employee/{employeeid}/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly ILeaveRequestRepository leaveRequestRepo;

        public LeaveRequestController(IMapper mapper, ILeaveRequestRepository leaveRequestRepo)
        {
            this.mapper = mapper;
            this.leaveRequestRepo = leaveRequestRepo;
        }

        [HttpGet]
        public ActionResult<IEnumerable<LeaveRequestReadDto>> GetEmployeeLeaveRequests(Guid employeeid)
        {
            Console.WriteLine($"--> Getting All Leave-Requets for employee: {employeeid}");

            if (!leaveRequestRepo.EmployeeExist(employeeid))
            {
                return NotFound();
            }

            var leaves = leaveRequestRepo.GetEmployeeLeaveRequests(employeeid);

            if (leaves == null)
                return NotFound();

            return Ok(mapper.Map<IEnumerable<LeaveRequestReadDto>>(leaves));
        }

        [Route("{leaveRequestID}", Name = "GetEmployeeLeaveRequest")]
        [HttpGet]
        public ActionResult<LeaveRequestReadDto> GetEmployeeLeaveRequest(Guid employeeID, int leaveRequestID)
        {
            Console.WriteLine($"--> Getting All Leave-Requets for employee: {employeeID}");

            if (!leaveRequestRepo.EmployeeExist(employeeID))
            {
                return NotFound();
            }

            var leaves = leaveRequestRepo.GetLeaveRequest(employeeID, leaveRequestID);

            if (leaves == null)
                return NotFound();

            return Ok(mapper.Map<LeaveRequestReadDto>(leaves));
        }

        [HttpPost]
        public ActionResult<LeaveRequestReadDto> CreateLeaveRequest(Guid employeeID, LeaveRequestCreateDto leaveDto)
        {
            Console.WriteLine($"--> Create Leave-Requets for employee: {employeeID}");

            if (!leaveRequestRepo.EmployeeExist(employeeID))
            {
                return NotFound();
            }

            var request = mapper.Map<LeaveRequest>(leaveDto);
            var requestDto = new LeaveRequestReadDto();
            try
            {
                leaveRequestRepo.CreateLeaveRequest(employeeID, request);

                requestDto = mapper.Map<LeaveRequestReadDto>(request);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Error while Creating Leave-Requets: {ex}");
            }

            return CreatedAtRoute(nameof(GetEmployeeLeaveRequest),
                new { employeeID = employeeID, leaverequestID = requestDto.LeaveRequestID }, requestDto);
        }

        //[HttpPost]
        //public ActionResult TestInboundConnection()
        //{
        //    Console.WriteLine("--> Inbound POST # leave request service");

        //    return Ok("### Inbound test from Employee Controller ###");
        //}
    }
}

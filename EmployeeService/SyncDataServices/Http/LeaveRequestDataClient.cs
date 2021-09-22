using EmployeeService.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EmployeeService.SyncDataServices.Http
{
    public class LeaveRequestDataClient : ILeaveRequestDataClient
    {
        private readonly HttpClient httpClient;
        private readonly IConfiguration configuration;

        public LeaveRequestDataClient(HttpClient httpClient, IConfiguration configuration)
        {
            this.httpClient = httpClient;
            this.configuration = configuration;
        }

        public async Task SendEmployeeToLeaveRequest(EmployeeReadDto employee)
        {
            var httpcontent = new StringContent(
                JsonSerializer.Serialize(employee),
                Encoding.UTF8,
                "application/json");
            
            var response = await httpClient.PostAsync($"{configuration["LeaveRequestService"]}/api/leave/LeaveRequest", httpcontent);

            if (response.IsSuccessStatusCode)
            {
                Console.WriteLine("--> sync POST to LeaveRequest Service was OK");
            }
            else
            {
                Console.WriteLine("--> sync POST to LeaveRequest Service was NOT OK XXXXXXX");
            }
        }
    }
}
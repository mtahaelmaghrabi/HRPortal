using EmployeeService.Dtos;

namespace EmployeeService.AsyncDataServices
{
    public interface IMessageBusClient
    {
        void PublishNewEmployee(EmployeePublishDto employeePublishDto);
        void SendMessage(string message);
    }
}
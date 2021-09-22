using EmployeeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeService.AzureMessaging
{
    public interface IMessageBus
    {
        Task PublishMessage(EmployeePublishDto message, string topicName);
    }
}
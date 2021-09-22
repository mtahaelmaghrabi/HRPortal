using EmployeeService.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using System.Text.Json;
using System.Text;

namespace EmployeeService.AsyncDataServices
{
    public class MessageBusClient : IMessageBusClient
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        public IConfiguration configuration { get; }

        public MessageBusClient(IConfiguration configuration)
        {
            // to setup the connection
            this.configuration = configuration;
            var factory = new ConnectionFactory()
            {
                HostName = configuration["RabbitMQHost"],
                Port = int.Parse(configuration["RabbitMQPort"])
            };
            try
            {
                _connection = factory.CreateConnection();

                _channel = _connection.CreateModel();

                _channel.ExchangeDeclare(exchange: configuration["ExchangeName"], type: ExchangeType.Fanout);

                _connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;

                Console.WriteLine($"--> Connecting to RabbitMQ at: {DateTime.Now}");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"--> Could not connect to the message bus: {ex.Message}");
            }
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> The RabbitMQ connection Shutdown at: {DateTime.Now}");
        }

        private void Dispose()
        {
            Console.WriteLine("--> RabbitMQ Message bus disposed");
            if (_connection.IsOpen)
            {
                _channel.Close();
                _connection.Close();
            }
        }

        public void PublishNewEmployee(EmployeePublishDto employeePublishDto)
        {
            var message = JsonSerializer.Serialize(employeePublishDto);

            if (_connection.IsOpen)
            {
                Console.WriteLine($"--> RabbitMQ connection Open for Sending Message at: {DateTime.Now}");

                SendMessage(message);
            }
            else
            {
                Console.WriteLine("--> RabbitMQ connection Closed, Not Sending Message");
            }
        }

        public void SendMessage(string message)
        {
            var messageBody = Encoding.UTF8.GetBytes(message);

            _channel.BasicPublish(
                exchange: configuration["ExchangeName"],
                routingKey: "",
                basicProperties: null,
                body: messageBody);

            Console.WriteLine($"--> We have sent a message to RabbitMQ: {message}");
        }
    }
}
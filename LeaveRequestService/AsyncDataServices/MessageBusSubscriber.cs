using LeaveRequestService.EventProcessing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LeaveRequestService.AsyncDataServices
{
    public class MessageBusSubscriber : BackgroundService
    {
        private IConnection connection;
        private IModel channel;
        private string queueName;

        private IConfiguration configuration { get; }
        private IEventProcessor eventProcessor { get; }

        public MessageBusSubscriber(IConfiguration configuration, IEventProcessor eventProcessor)
        {
            this.configuration = configuration;
            this.eventProcessor = eventProcessor;

            IntializeRabbitMQ();
        }

        private void IntializeRabbitMQ()
        {
            var factory = new ConnectionFactory() { HostName = configuration["RabbitMQHost"], Port = int.Parse(configuration["RabbitMQPort"]) };
            connection = factory.CreateConnection();
            channel = connection.CreateModel();
            channel.ExchangeDeclare(exchange: configuration["ExchangeName"], type: ExchangeType.Fanout);
            queueName = channel.QueueDeclare().QueueName;
            channel.QueueBind(queue: queueName, exchange: configuration["ExchangeName"], routingKey: "");

            Console.WriteLine($"--> Listening on the message bus ...");

            connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        private void RabbitMQ_ConnectionShutdown(object sender, ShutdownEventArgs e)
        {
            Console.WriteLine($"--> RabbitMQ Connection Shutdown");

        }

        public override void Dispose()
        {
            if (channel.IsOpen)
            {
                channel.Close();
                connection.Close();
            }
            base.Dispose();
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            var consumer = new EventingBasicConsumer(channel);

            // The Magic happens here ...
            consumer.Received += (ModuleHandle, ea) =>
              {
                  Console.WriteLine($"--> RabbitMQ Event Received");

                  var body = ea.Body;
                  var notificationMessage = Encoding.UTF8.GetString(body.ToArray());

                  eventProcessor.ProcessEvent(notificationMessage);
              };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            return Task.CompletedTask;
        }
    }
}
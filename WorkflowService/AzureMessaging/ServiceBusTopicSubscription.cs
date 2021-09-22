using Azure.Messaging.ServiceBus;
using Azure.Messaging.ServiceBus.Administration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WorkflowService.AzureMessaging.Dtos;

namespace WorkflowService.AzureMessaging
{
    public class ServiceBusTopicSubscription : IServiceBusTopicSubscription
    {
        private readonly IProcessData _processData;
        private readonly IConfiguration _configuration;
        private string TOPIC_PATH = "";
        private string SUBSCRIPTION_NAME = "";
        string connectionString = "";
        private readonly ILogger _logger;
        private readonly ServiceBusClient _client;
        private readonly ServiceBusAdministrationClient _adminClient;
        private ServiceBusProcessor _processor;

        public ServiceBusTopicSubscription(IProcessData processData,
            IConfiguration configuration,
            ILogger<ServiceBusTopicSubscription> logger)
        {
            _processData = processData;
            _configuration = configuration;
            _logger = logger;

            TOPIC_PATH = _configuration["AzureServiceBusTopicName"];
            SUBSCRIPTION_NAME = _configuration["SubscriptionName"];
            connectionString = _configuration["AzureServiceBusConnection"];

            _client = new ServiceBusClient(connectionString);
            _adminClient = new ServiceBusAdministrationClient(connectionString);
        }

        public async Task PrepareFiltersAndHandleMessages()
        {
            ServiceBusProcessorOptions _serviceBusProcessorOptions = new ServiceBusProcessorOptions
            {
                MaxConcurrentCalls = 1,
                AutoCompleteMessages = false,
            };

            _processor = _client.CreateProcessor(TOPIC_PATH, SUBSCRIPTION_NAME, _serviceBusProcessorOptions);
            _processor.ProcessMessageAsync += ProcessMessagesAsync;
            _processor.ProcessErrorAsync += ProcessErrorAsync;

            await RemoveDefaultFilters();
            await AddFilters();

            await _processor.StartProcessingAsync().ConfigureAwait(false);
        }

        private async Task RemoveDefaultFilters()
        {
            try
            {
                var rules = _adminClient.GetRulesAsync(TOPIC_PATH, SUBSCRIPTION_NAME);
                var ruleProperties = new List<RuleProperties>();
                await foreach (var rule in rules)
                {
                    ruleProperties.Add(rule);
                }

                foreach (var rule in ruleProperties)
                {
                    if (rule.Name == "GoalsGreaterThanSeven")
                    {
                        await _adminClient.DeleteRuleAsync(TOPIC_PATH, SUBSCRIPTION_NAME, "GoalsGreaterThanSeven")
                            .ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
            }
        }

        private async Task AddFilters()
        {
            try
            {
                var rules = _adminClient.GetRulesAsync(TOPIC_PATH, SUBSCRIPTION_NAME)
                    .ConfigureAwait(false);

                var ruleProperties = new List<RuleProperties>();
                await foreach (var rule in rules)
                {
                    ruleProperties.Add(rule);
                }

                if (!ruleProperties.Any(r => r.Name == "GoalsGreaterThanSeven"))
                {
                    CreateRuleOptions createRuleOptions = new CreateRuleOptions
                    {
                        Name = "GoalsGreaterThanSeven",
                        Filter = new SqlRuleFilter("goals > 7")
                    };
                    await _adminClient.CreateRuleAsync(TOPIC_PATH, SUBSCRIPTION_NAME, createRuleOptions)
                        .ConfigureAwait(false);
                }
            }
            catch (Exception ex)
            {
                _logger.LogWarning(ex.ToString());
            }
        }

        private async Task ProcessMessagesAsync(ProcessMessageEventArgs args)
        {

            var notificationMessage = Encoding.UTF8.GetString(args.Message.Body.ToArray());

            var employeePublishedDto = JsonSerializer.Deserialize<EmployeePublishDto>(notificationMessage);


            var myPayload = args.Message.Body.ToObjectFromJson<EmployeePublishDto>();
            await _processData.Process(myPayload).ConfigureAwait(false);
            await args.CompleteMessageAsync(args.Message).ConfigureAwait(false);
        }

        private Task ProcessErrorAsync(ProcessErrorEventArgs arg)
        {
            _logger.LogError(arg.Exception, "Message handler encountered an exception");
            _logger.LogDebug($"- ErrorSource: {arg.ErrorSource}");
            _logger.LogDebug($"- Entity Path: {arg.EntityPath}");
            _logger.LogDebug($"- FullyQualifiedNamespace: {arg.FullyQualifiedNamespace}");

            return Task.CompletedTask;
        }

        public async ValueTask DisposeAsync()
        {
            if (_processor != null)
            {
                await _processor.DisposeAsync().ConfigureAwait(false);
            }

            if (_client != null)
            {
                await _client.DisposeAsync().ConfigureAwait(false);
            }
        }

        public async Task CloseQueueAsync()
        {
            await _processor.CloseAsync().ConfigureAwait(false);
        }
    }
}
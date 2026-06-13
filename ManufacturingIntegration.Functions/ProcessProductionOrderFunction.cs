using System;
using System.Text.Json;
using Azure.Messaging.ServiceBus;
using ManufacturingIntegration.Functions.Services;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Shared.Contracts.Event;

namespace ManufacturingIntegration.Functions
{
    public class ProcessProductionOrderFunction
    {
        private readonly ILogger<ProcessProductionOrderFunction> _logger;
        private readonly IMesApiClient _mesApiClient;

        public ProcessProductionOrderFunction(ILogger<ProcessProductionOrderFunction> logger, IMesApiClient mesApiClient)
        {
            _logger = logger;
            _mesApiClient = mesApiClient;
        }

        [Function(nameof(ProcessProductionOrderFunction))]
        public async Task Run([ServiceBusTrigger("production-order-released", Connection = "ServiceBusConnection")] ServiceBusReceivedMessage message)
        {
            _logger.LogInformation("Delivery Count: {count}",message.DeliveryCount);

            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            var productionOrder = JsonSerializer.Deserialize<ProductionOrderReleasedEvent>(message.Body.ToString());

            if (productionOrder is null)
            {
                _logger.LogError("Failed to deserialize message.");
                return;
            }

            await _mesApiClient.CreateJobAsync(productionOrder!);

            _logger.LogInformation("MES Job created successfully.");
        }
    }
}

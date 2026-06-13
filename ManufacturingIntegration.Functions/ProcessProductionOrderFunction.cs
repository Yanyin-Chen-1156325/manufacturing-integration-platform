using System;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ManufacturingIntegration.Functions
{
    public class ProcessProductionOrderFunction
    {
        private readonly ILogger<ProcessProductionOrderFunction> _logger;

        public ProcessProductionOrderFunction(ILogger<ProcessProductionOrderFunction> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessProductionOrderFunction))]
        public void Run([ServiceBusTrigger("production-order-released", Connection = "ServiceBusConnection")] ServiceBusReceivedMessage message)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);
        }
    }
}

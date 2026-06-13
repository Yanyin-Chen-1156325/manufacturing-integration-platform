using System.Text.Json;
using Azure.Messaging.ServiceBus;

namespace Erp.Api.Services
{
    public class ServiceBusPublisher : IServiceBusPublisher
    {
        private readonly ServiceBusClient _client;
        private readonly IConfiguration _configuration;

        public ServiceBusPublisher(
            IConfiguration configuration)
        {
            _configuration = configuration;

            _client = new ServiceBusClient(
                configuration["ServiceBus:ConnectionString"]);
        }

        public async Task PublishAsync<T>(T message)
        {
            var queueName =
                _configuration["ServiceBus:QueueName"];

            var sender =
                _client.CreateSender(queueName);

            var json =
                JsonSerializer.Serialize(message);

            await sender.SendMessageAsync(
                new ServiceBusMessage(json));
        }
    }
}

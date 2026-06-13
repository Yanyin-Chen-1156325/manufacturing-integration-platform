using Azure.Messaging.ServiceBus;
using ManufacturingIntegration.Functions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        // MES API Client
        services.AddHttpClient<IMesApiClient, MesApiClient>(
            client =>
            {
                client.BaseAddress = new Uri(
                    context.Configuration["MesApiBaseUrl"]!);
            });

        // Service Bus Client
        services.AddSingleton(
            new ServiceBusClient(
                context.Configuration["ServiceBusConnection"]!));

    })
    .Build();

host.Run();

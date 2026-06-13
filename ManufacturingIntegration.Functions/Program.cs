using ManufacturingIntegration.Functions.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var host = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults()
    .ConfigureServices((context, services) =>
    {
        services.AddHttpClient<IMesApiClient, MesApiClient>(
            client =>
            {
                client.BaseAddress = new Uri(
                    context.Configuration["MesApiBaseUrl"]!);
            });
    })
    .Build();

host.Run();

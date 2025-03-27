using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Subscriptor.Config;
using Subscriptor.Dtos;
using Subscriptor.Services;
using System.Text.Json;

class Program
{
    static async Task Main()
    {
        // 1. Crear HostBuilder para cargar configuración desde user secrets
        var builder = Host.CreateDefaultBuilder()
            .ConfigureAppConfiguration((context, config) =>
            {
                config.AddUserSecrets<Program>(); // <- Esto habilita user secrets
            })
            .ConfigureServices((context, services) =>
            {
                services.Configure<ServiceBusConfig>(
                    context.Configuration.GetSection("ServiceBus"));

                services.AddTransient<EjecutarPagoService>();
            });

        var host = builder.Build();

        // 2. Obtener configuración de Service Bus
        var config = host.Services.GetRequiredService<IOptions<ServiceBusConfig>>().Value;
        var processorService = host.Services.GetRequiredService<EjecutarPagoService>();

        // 3. Iniciar procesamiento del topic
        await using var client = new ServiceBusClient(config.ConnectionString);
        var processor = client.CreateProcessor(config.TopicName, config.SubscriptionName);

        processor.ProcessMessageAsync += async args =>
        {
            var body = args.Message.Body.ToString();
            var request = JsonSerializer.Deserialize<PagoDeNominaRequest>(body);
            await processorService.ProcessAsync(request);
            await args.CompleteMessageAsync(args.Message);
        };

        processor.ProcessErrorAsync += async args =>
        {
            Console.WriteLine($"⚠️ Error: {args.Exception.Message}");
            await Task.CompletedTask;
        };

        await processor.StartProcessingAsync();
        Console.WriteLine("⏳ Listening for payroll requests...");
        Console.ReadKey();
    }
}
using Azure.Messaging.ServiceBus;
using Microsoft.Extensions.Options;
using Publicador.Config;
using Publicador.Dtos;
using System.Text.Json;


namespace Publicador.Services
{
    public class PagoNominaServices
    {
        private readonly ServiceBusConfig _config;
        public PagoNominaServices(IOptions<ServiceBusConfig> config)
        {
            _config = config.Value;
        }
        public async Task PublishAsync(PagoNominaRequest request)
        {
            var client = new ServiceBusClient(_config.connectionString);
            var sender = client.CreateSender(_config.topicName);

            string jsonMessage = JsonSerializer.Serialize(request);
            var message = new ServiceBusMessage(jsonMessage);

            await sender.SendMessageAsync(message);
        
    }
}
}

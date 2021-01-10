using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LZMotel.Clientes.API.Services;
using LZMotel.Core.Utils;
using LZMotel.MessageBus;

namespace LZMotel.Clientes.API.Configuration
{
    public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"))
                .AddHostedService<RegistroClienteIntegrationHandler>();
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LZMotel.Core.Utils;
using LZMotel.MessageBus;

namespace LZMotel.Usuarios.API.Configuration
{
  public static class MessageBusConfig
    {
        public static void AddMessageBusConfiguration(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddMessageBus(configuration.GetMessageQueueConnection("MessageBus"));
        }
    }
}
using FluentValidation.Results;
using LZMotel.Clientes.API.Application.Commands;
using LZMotel.Clientes.API.Application.Events;
using LZMotel.Clientes.API.Data;
using LZMotel.Clientes.API.Data.Repository;
using LZMotel.Clientes.API.Models;
using LZMotel.Core.Mediator;
using LZMotel.WebAPI.Core.Usuario;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace LZMotel.Clientes.API.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services)
    {
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IAspNetUser, AspNetUser>();

      services.AddScoped<IMediatorHandler, MediatorHandler>();
      services.AddScoped<IRequestHandler<RegistrarClienteCommand, ValidationResult>, ClienteCommandHandler>();

      services.AddScoped<INotificationHandler<ClienteRegistradoEvent>, ClienteEventHandler>();

      services.AddScoped<IClienteRepository, ClienteRepository>();
      services.AddScoped<ClienteContext>();
    }
  }
}
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LZMotel.Clientes.API.Application.Commands;
using LZMotel.Core.Mediator;
using LZMotel.Core.Messages.Integration;
using LZMotel.MessageBus;

namespace LZMotel.Clientes.API.Services
{
  public class RegistroClienteIntegrationHandler : BackgroundService
  {
    private IMessageBus _bus;
    private readonly IServiceProvider _serviceProvider;

    public RegistroClienteIntegrationHandler(IServiceProvider serviceProvider, IMessageBus bus)
    {
      _serviceProvider = serviceProvider;
      _bus = bus;
    }

    private void SetResponder()
    {
      _bus.RespondAsync<UsuarioRegistradoIntegrationEvent, ResponseMessage>(async request =>
          await RegistrarCliente(request));

      _bus.AdvancedBus.Connected += OnConnect;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
      SetResponder();
      return Task.CompletedTask;
    }

    private void OnConnect(object s, EventArgs e)
    {
      SetResponder();
    }

    private async Task<ResponseMessage> RegistrarCliente(UsuarioRegistradoIntegrationEvent message)
    {
      var clienteCommand = new RegistrarClienteCommand(message.Id, message.Nome, message.Turno, 
        message.Email, message.Cpf);

      ValidationResult sucesso;

      using (var scope = _serviceProvider.CreateScope())
      {
        var mediator = scope.ServiceProvider.GetRequiredService<IMediatorHandler>();
        sucesso = await mediator.EnviarComando(clienteCommand);
      }

      return new ResponseMessage(sucesso);
    }
  }
}
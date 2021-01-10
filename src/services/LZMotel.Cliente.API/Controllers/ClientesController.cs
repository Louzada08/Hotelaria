using LZMotel.Clientes.API.Models;
using LZMotel.Core.Mediator;
using LZMotel.WebAPI.Core.Controllers;
using LZMotel.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LZMotel.Clientes.API.Controllers
{
  public class ClientesController : MainController
  {
    private readonly IClienteRepository _clienteRepository;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IAspNetUser _user;

    public ClientesController(IMediatorHandler mediatorHandler, 
      IClienteRepository clienteRepository, IAspNetUser user)
    {
      _mediatorHandler = mediatorHandler;
      _clienteRepository = clienteRepository;
      _user = user;
    }


    [HttpGet("cliente/{id}")]
    public async Task<Cliente> ObterCliente(Guid id)
    {
      return await _clienteRepository.ObterClientePorId(id);
    }

    [HttpGet("clientes/usuarios")]
    public async Task<IEnumerable<Cliente>> ObterListaClientes()
    {
      return await _clienteRepository.ObterListaClientes();
    }
  }
}
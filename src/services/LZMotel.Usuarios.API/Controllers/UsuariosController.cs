using LZMotel.Clientes.API.Models;
using LZMotel.Core.Mediator;
using LZMotel.WebAPI.Core.Controllers;
using LZMotel.WebAPI.Core.Usuario;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LZMotel.Usuarios.API.Controllers
{
  public class UsuariosController : MainController
  {
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly IMediatorHandler _mediatorHandler;
    private readonly IAspNetUser _user;

    public UsuariosController(IMediatorHandler mediatorHandler,
      IUsuarioRepository usuarioRepository, IAspNetUser user)
    {
      _mediatorHandler = mediatorHandler;
      _usuarioRepository = usuarioRepository;
      _user = user;
    }


    [HttpGet("cliente/{id}")]
    public async Task<Cliente> ObterCliente(Guid id)
    {
      return await _usuarioRepository.ObterClientePorId(id);
    }

    [HttpGet("clientes/usuarios")]
    public async Task<IEnumerable<Cliente>> ObterListaClientes()
    {
      return await _usuarioRepository.ObterListaClientes();
    }
  }
}
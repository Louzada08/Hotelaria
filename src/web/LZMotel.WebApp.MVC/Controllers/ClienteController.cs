using LZMotel.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LZMotel.WebApp.MVC.Controllers
{
  [Authorize]
  public class ClienteController : MainController
  {
    private readonly IClienteService _clienteService;

    public ClienteController(IClienteService clienteService)
    {
      _clienteService = clienteService;
    }

    [HttpGet]
    [Route("cliente/{id}")]
    public async Task<IActionResult> UsuarioIndex(Guid id)
    {
      var cliente = await _clienteService.ObterRegistroCliente(id);

      //if (ResponsePossuiErros(response) TempData["Erros"] =
      //    ModelState.Values.SelectMany(v => v.Errors.Select(e => e.ErrorMessage)).ToList();
      //return View("Index", await _comprasBffService.ObterCarrinho());
      //return RedirectToAction("Usuario","Identidade",cliente);
      return View(cliente);
    }

    [HttpGet]
    [Route("novo-usuario")]
    public IActionResult Registro(string returnUrl = null)
    {
      return View();
    }

  }
}

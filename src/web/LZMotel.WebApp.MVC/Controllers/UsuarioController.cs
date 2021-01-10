using LZMotel.WebApp.MVC.Models;
using LZMotel.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace LZMotel.WebApp.MVC.Controllers
{
  public class UsuarioController : MainController
  {
    private readonly IAutenticacaoService _autenticacaoService;
    private readonly IClienteService _clienteService;
    private readonly IUsuarioService _usuarioService;
    public UsuarioController(IAutenticacaoService autenticacaoService, IClienteService clienteService,
      IUsuarioService usuarioService)
    {
      _autenticacaoService = autenticacaoService;
      _clienteService = clienteService;
      _usuarioService = usuarioService;
    }

    [HttpGet]
    [Route("usuarios")]
    public async Task<IActionResult> Index()
    {
      return View(await _clienteService.ObterTodosClientes());
    }

    [HttpPost]
    [Route("nova-conta")]
    public async Task<IActionResult> Registro(UsuarioRegistro usuarioRegistro, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;

      if (!ModelState.IsValid) return View(usuarioRegistro);

      var resposta = await _autenticacaoService.Registro(usuarioRegistro);

      if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioRegistro);

      //await RealizarLogin(resposta);

      return RedirectToAction("Usuario", controllerName: "Identidade",fragment: "registro");
      //return LocalRedirect("novo-usurio");
    }

  }
}

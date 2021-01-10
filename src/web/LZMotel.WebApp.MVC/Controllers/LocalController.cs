using LZMotel.WebApp.MVC.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LZMotel.WebApp.MVC.Controllers
{
  public class LocalController : MainController
  {
    private readonly ILocalService _localService;

    public LocalController(ILocalService localService)
    {
      _localService = localService;
    }

    [HttpGet]
    [Route("painel")]
    public async Task<IActionResult> Index()
    {
      var suites = await _localService.ObterTodos();

      return View(suites);
    }


    [HttpGet]
    [Route("suite-detalhe/{id}")]
    public async Task<IActionResult> SuiteDetalhe(Guid id)
    {
      var suite = await _localService.ObterPorId(id);

      return View(suite);
    }
  }
}

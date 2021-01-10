using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LZMotel.Local.API.Models;
using LZMotel.WebAPI.Core.Controllers;
using LZMotel.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LZMotel.Local.API.Controllers
{
  [Authorize]
  public class LocalController : MainController
  {
    private readonly ISuiteRepository _SuiteRepository;

    public LocalController(ISuiteRepository SuiteRepository)
    {
      _SuiteRepository = SuiteRepository;
    }

    [AllowAnonymous]
    [HttpGet("local/suites")]
    public async Task<IEnumerable<Suite>> Index()
    {
      return await _SuiteRepository.ObterTodos();
    }

    [ClaimsAuthorize("NivelDeAcesso", "GERENTE")]
    [HttpGet("local/Suites/{id}")]
    public async Task<Suite> SuiteDetalhe(Guid id)
    {
      return await _SuiteRepository.ObterPorId(id);
    }
  }
}
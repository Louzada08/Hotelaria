using LZMotel.WebApp.MVC.Models;
using LZMotel.WebApp.MVC.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LZMotel.WebApp.MVC.Controllers
{
  public class IdentidadeController : MainController
  {
    private readonly IAutenticacaoService _autenticacaoService;
    public IdentidadeController(IAutenticacaoService autenticacaoService)
    {
      _autenticacaoService = autenticacaoService;
    }

    [HttpGet]
    [Route("")]
    [Route("login")]
    public IActionResult login(string returnUrl = null)
    {
      
      ViewData["ReturnUrl"] = returnUrl;
      return View();
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(UsuarioLogin usuarioLogin, string returnUrl = null)
    {
      ViewData["ReturnUrl"] = returnUrl;

      if (!ModelState.IsValid) return View(usuarioLogin);

      var resposta = await _autenticacaoService.Login(usuarioLogin);

      if (ResponsePossuiErros(resposta.ResponseResult)) return View(usuarioLogin);

      await RealizarLogin(resposta);

      if (string.IsNullOrEmpty(returnUrl)) return RedirectToAction("Index", controllerName: "Local");

      return LocalRedirect(returnUrl);
    }

    [HttpGet]
    [Route("sair")]
    public async Task<IActionResult> Logout()
    {
      await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
      return LocalRedirect("/login/");
    }

    private async Task RealizarLogin(UsuarioRespostaLogin resposta)
    {
      var token = ObterTokenFormatado(resposta.AccessToken);

      var claims = new List<Claim>();
      claims.Add(new Claim("JWT", resposta.AccessToken));
      claims.AddRange(token.Claims);
      
      var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

      var authProperties = new AuthenticationProperties
      {
        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(60),
        IsPersistent = true
      };

      await HttpContext.SignInAsync(
          CookieAuthenticationDefaults.AuthenticationScheme,
          new ClaimsPrincipal(claimsIdentity),
          authProperties);
    }

    private static JwtSecurityToken ObterTokenFormatado(string jwtToken)
    {
      return new JwtSecurityTokenHandler().ReadToken(jwtToken) as JwtSecurityToken;
    }
  }
}

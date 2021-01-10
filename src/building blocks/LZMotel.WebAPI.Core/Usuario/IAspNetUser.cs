using System;
using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace LZMotel.WebAPI.Core.Usuario
{
  public interface IAspNetUser
  {
    string Name { get; }
    Guid ObterUserId();
    string ObterTurno(Guid id);
    string ObterUserEmail();
    string ObterUserToken();
    bool EstaAutenticado();
    bool PossuiRole(string role);
    IEnumerable<Claim> ObterClaims();
    HttpContext ObterHttpContext();
  }
}
using LZMotel.Clientes.API.Models;
using LZMotel.WebApp.MVC.Extensions;
using LZMotel.WebApp.MVC.Models;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace LZMotel.WebApp.MVC.Services
{
  #region Interface
  public interface IClienteService
  {
    Task<ClienteViewModel> ObterRegistroCliente(Guid id);
    Task<IEnumerable<ClienteViewModel>> ObterTodosClientes();
  }
  #endregion
  public class ClienteService : Service, IClienteService
  {
    private readonly HttpClient _httpClient;

    public ClienteService(HttpClient httpClient, IOptions<AppSettings> settings)
    {
      _httpClient = httpClient;
      httpClient.BaseAddress = new Uri(settings.Value.ClienteUrl);
    }

    public async Task<ClienteViewModel> ObterRegistroCliente(Guid id)
    {
      var response = await _httpClient.GetAsync(requestUri: $"/cliente/usuario/{id}");

      if (response.StatusCode == HttpStatusCode.NotFound) return null;

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<ClienteViewModel>(response);
    }

    public async Task<IEnumerable<ClienteViewModel>> ObterTodosClientes()
    {
      var response = await _httpClient.GetAsync(requestUri: "/clientes/usuarios/");

      if (response.StatusCode == HttpStatusCode.NotFound) return null;

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<IEnumerable<ClienteViewModel>>(response);
    }
  }
}
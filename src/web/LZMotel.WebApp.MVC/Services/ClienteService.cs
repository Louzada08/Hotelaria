using LZMotel.Core.DomainObjects;
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
    Task<UsuarioRegistro> ObterTodosClientes();
    Task<EnderecoViewModel> ObterEndereco();
    Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco);

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

    public async Task<UsuarioRegistro> ObterTodosClientes()
    {
      var response = await _httpClient.GetAsync(requestUri: "/clientes/usuarios/");

      if (response.StatusCode == HttpStatusCode.NotFound) return null;

      TratarErrosResponse(response);

      var clientesUsers = new UsuarioRegistro
      {
        Clientes = await DeserializarObjetoResponse<IEnumerable<ClienteViewModel>>(response)
      }; 
      
      return clientesUsers;
    }

    public async Task<EnderecoViewModel> ObterEndereco()
    {
      var response = await _httpClient.GetAsync("/cliente/endereco/");

      if (response.StatusCode == HttpStatusCode.NotFound) return null;

      TratarErrosResponse(response);

      return await DeserializarObjetoResponse<EnderecoViewModel>(response);
    }

    public async Task<ResponseResult> AdicionarEndereco(EnderecoViewModel endereco)
    {
      var enderecoContent = ObterConteudo(endereco);

      var response = await _httpClient.PostAsync("/cliente/endereco/", enderecoContent);

      if (!TratarErrosResponse(response)) return await DeserializarObjetoResponse<ResponseResult>(response);

      return RetornoOk();
    }

  }
}
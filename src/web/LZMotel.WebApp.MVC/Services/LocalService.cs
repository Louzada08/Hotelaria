using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using LZMotel.WebApp.MVC.Extensions;
using LZMotel.WebApp.MVC.Models;
using Microsoft.Extensions.Options;

namespace LZMotel.WebApp.MVC.Services
{
  #region Interface
  public interface ILocalService
  {
    Task<IEnumerable<SuiteViewModel>> ObterTodos();
    Task<SuiteViewModel> ObterPorId(Guid id);
  }
  #endregion

  public class LocalService : Service, ILocalService
    {
        private readonly HttpClient _httpClient;

        public LocalService(HttpClient httpClient,
            IOptions<AppSettings> settings)
        {
            httpClient.BaseAddress = new Uri(settings.Value.LocalUrl);

            _httpClient = httpClient;
        }

        public async Task<SuiteViewModel> ObterPorId(Guid id)
        {
            var response = await _httpClient.GetAsync($"/local/suites/{id}");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<SuiteViewModel>(response);
        }

        public async Task<IEnumerable<SuiteViewModel>> ObterTodos()
        {
            var response = await _httpClient.GetAsync("/local/suites/");

            TratarErrosResponse(response);

            return await DeserializarObjetoResponse<IEnumerable<SuiteViewModel>>(response);
        }
    }
}
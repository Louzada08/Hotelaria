using System;
using System.Net.Http;
using LZMotel.WebApp.MVC.Extensions;
using LZMotel.WebApp.MVC.Services;
using LZMotel.WebApp.MVC.Services.Handlers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Polly;
using Polly.Extensions.Http;
using Polly.Retry;

namespace LZMotel.WebApp.MVC.Configuration
{
  public static class DependencyInjectionConfig
  {
    public static void RegisterServices(this IServiceCollection services, IConfiguration configuration)
    {
      services.AddSingleton<IValidationAttributeAdapterProvider, CpfValidationAttributeAdapterProvider>();
      services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      services.AddScoped<IUser, AspNetUser>();

      services.AddTransient<HttpClientAuthorizationDelegatingHandler>();

      services.AddHttpClient<IAutenticacaoService, AutenticacaoService>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(3, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<ILocalService, LocalService>()
          .AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(3, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<IClienteService, ClienteService>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(3, TimeSpan.FromSeconds(30)));

      services.AddHttpClient<IUsuarioService, UsuarioService>()
          .AddPolicyHandler(PollyExtensions.EsperarTentar())
          .AddTransientHttpErrorPolicy(
              p => p.CircuitBreakerAsync(3, TimeSpan.FromSeconds(30)));

    }
  }

  public class PollyExtensions
  {
    public static AsyncRetryPolicy<HttpResponseMessage> EsperarTentar()
    {
      var retry = HttpPolicyExtensions
          .HandleTransientHttpError()
          .WaitAndRetryAsync(new[]
          {
                    TimeSpan.FromSeconds(1),
                    TimeSpan.FromSeconds(5),
                    TimeSpan.FromSeconds(10),
          }, (outcome, timespan, retryCount, context) =>
          {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine($"Tentando pela {retryCount} vez!");
            Console.ForegroundColor = ConsoleColor.White;
          });

      return retry;
    }
  }
}
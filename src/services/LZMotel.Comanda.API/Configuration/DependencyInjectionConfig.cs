using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using LZMotel.Carrinho.API.Data;
using LZMotel.WebAPI.Core.Usuario;

namespace LZMotel.Carrinho.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IAspNetUser, AspNetUser>();
            services.AddScoped<ComandaContext>();
        }
    }
}
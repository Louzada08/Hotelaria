using LZMotel.Local.API.Data;
using LZMotel.Local.API.Data.Repository;
using LZMotel.Local.API.Models;
using Microsoft.Extensions.DependencyInjection;

namespace LZMotel.Local.API.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ISuiteRepository, SuiteRepository>();
            services.AddScoped<LocalContext>();
        }
    }
}
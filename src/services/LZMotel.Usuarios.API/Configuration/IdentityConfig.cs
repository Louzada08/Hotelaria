using LZMotel.Usuarios.API.Data;
using LZMotel.Usuarios.API.Extensions;
using LZMotel.WebAPI.Core.Identidade;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LZMotel.Usuarios.API.Configuration
{
  public static class IdentityConfig
  {
    public static IServiceCollection AddIdentityConfiguration(this IServiceCollection services,
        IConfiguration configuration)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
          options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

      services.AddDefaultIdentity<IdentityUser>()
          .AddRoles<IdentityRole>()
          .AddErrorDescriber<IdentityMensagensPortugues>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      services.AddJwtConfiguration(configuration);

      return services;
    }
  }
}
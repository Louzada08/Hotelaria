using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using LZMotel.Identidade.API.Data;
using LZMotel.Identidade.API.Extensions;
using LZMotel.WebAPI.Core.Identidade;

namespace LZMotel.Identidade.API.Configuration
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
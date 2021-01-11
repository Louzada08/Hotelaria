﻿using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace LZMotel.Usuarios.API.Configuration
{
  public static class SwaggerConfig
  {
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo()
        {
          Title = "LoveIn Usuario API",
          Description = "Esta API faz parte do Sistema Gestão de Motéis.",
          Contact = new OpenApiContact() { Name = "Anderson Luiz Louzada", Email = "valuz.anderson.to@gmail.com" },
          License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        });

      });

      return services;
    }

    public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app)
    {
      app.UseSwagger();
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
      });

      return app;
    }
  }
}
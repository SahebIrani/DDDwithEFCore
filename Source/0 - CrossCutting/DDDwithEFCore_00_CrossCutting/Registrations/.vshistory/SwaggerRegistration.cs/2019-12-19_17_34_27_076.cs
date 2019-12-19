using System;
using System.IO;
using System.Reflection;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace DDDwithEFCore_00_CrossCutting.Registrations
{
	public static class SwaggerRegistration
	{
		public static IApplicationBuilder UseSwaggerMiddleware(this IApplicationBuilder app)
		{
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/V1/swagger.json", "ProductCatalog API");
			});
			return app;
		}

		public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("V1", new OpenApiInfo
				{
					Title = "ProductCatalog API",
					Version = "V1",
					Description = "A simple example ASP.NET Core Web API",
				});

				// Set the comments path for the Swagger JSON and UI.
				var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
				var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
				c.IncludeXmlComments(xmlPath);
			});
			return services;
		}
	}
}

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
				c.SwaggerEndpoint(url: "/swagger/V1/swagger.json", name: "ProductCatalog API");
			});
			return app;
		}

		public static IServiceCollection AddSwaggerConfig(this IServiceCollection services)
		{
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("V1", new OpenApiInfo
				{
					Title = "SinjulMSBH API",
					Version = "V1",
					Description = "SinjulMSBH ASP.NET Core 3.1 Web API .. !!!!",
				});

				string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

				string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				c.IncludeXmlComments(xmlPath);
			});

			return services;
		}
	}
}

using System;
using System.IO;
using System.Reflection;

using DDDwithEFCore_00_CrossCutting.Infrastructures.JsonConverters;
using DDDwithEFCore_00_CrossCutting.Infrastructures.Middlewares;
using DDDwithEFCore_00_CrossCutting.Registrations;

using DDDwithEFCore_05_ApplicationServices.Commands.Infrastructure;

using DDDwithEFCore_06_ApplicationServices.Queries.Registrations;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DDDwithEFCore_07_Endpoint.WebApp
{
	public class Startup
	{
		public Startup(IConfiguration configuration) => Configuration = configuration;

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllersWithViews()
				.AddJsonOptions(configure =>
				{
					configure.JsonSerializerOptions.Converters.Add(item: new IdentityJsonConverterFactory());
				});

			services.AddSingleton<IConfiguration>(implementationFactory: sp => Configuration);
			services.AddApplicationCommands();
			services.AddApplicationQueries();
			//services.AddSwaggerConfig();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc(name: "V1", info: new OpenApiInfo
				{
					Title = "SinjulMSBH API",
					Version = "V1",
					Description = "SinjulMSBH ASP.NET Core 3.1 Web API .. !!!!",
				});

				string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";

				string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

				c.IncludeXmlComments(xmlPath);
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseDatabaseErrorPage();
			}
			else
			{
				//app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseSwaggerMiddleware();
			app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

			app.UseRouting();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}")
				;
			});
		}
	}
}

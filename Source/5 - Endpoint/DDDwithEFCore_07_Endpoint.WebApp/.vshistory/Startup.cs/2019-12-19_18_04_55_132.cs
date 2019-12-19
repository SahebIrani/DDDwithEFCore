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

			services.AddRazorPages();

			services.AddSingleton<IConfiguration>(implementationFactory: sp => Configuration);
			services.AddApplicationCommands();
			services.AddApplicationQueries();
			services.AddSwaggerConfig();
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
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}
			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseSwaggerMiddleware();
			app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

			app.UseRouting();

			app.UseAuthentication();
			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllerRoute(
					name: "default",
					pattern: "{controller=Home}/{action=Index}/{id?}")
				;

				endpoints.MapRazorPages();
			});
		}
	}
}

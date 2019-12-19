using System.IO;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DDDwithEFCore_07_Endpoint.WebApp
{
	public class Program
	{
		public static async Task Main(string[] args) => await CreateHostBuilder(args).Build().RunAsync();

		public static IHostBuilder CreateHostBuilder(string[] args) =>
			Host.CreateDefaultBuilder(args)
				.ConfigureWebHostDefaults(webBuilder =>
				{
					webBuilder.UseStartup<Startup>();
					webBuilder.ConfigureAppConfiguration((webHostBuilderContext, configurationBuilder) =>
					{
						configurationBuilder.SetBasePath(Directory.GetCurrentDirectory());
						configurationBuilder.AddJsonFile("appsettings.json");
						configurationBuilder.AddJsonFile(
							path: $"appsettings.{webHostBuilderContext.HostingEnvironment.EnvironmentName}.json",
							optional: true
						);
						configurationBuilder.AddEnvironmentVariables();
					});
					webBuilder.CaptureStartupErrors(true);
				})
				.UseDefaultServiceProvider((context, options) =>
				{
					options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
					options.ValidateOnBuild = true;
				});
	}
}

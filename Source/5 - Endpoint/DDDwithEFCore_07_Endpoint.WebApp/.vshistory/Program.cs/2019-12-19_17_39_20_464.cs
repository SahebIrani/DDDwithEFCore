using System.IO;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace DDDwithEFCore_07_Endpoint.WebApp
{
	public class Program
	{
		public static void Main(string[] args)
		{
			CreateHostBuilder(args).Build().Run();
		}

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

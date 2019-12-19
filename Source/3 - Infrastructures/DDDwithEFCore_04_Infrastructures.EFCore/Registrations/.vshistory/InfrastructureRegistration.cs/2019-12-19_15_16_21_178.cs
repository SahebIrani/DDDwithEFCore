using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Registrations
{
	public static class InfrastructureRegistration
	{
		public static IServiceCollection AddEfCoreSqlServerDb(this IServiceCollection services)
		{
			var svcProvider = services.BuildServiceProvider();

			services.Replace(
				ServiceDescriptor.Scoped<
					IDbConnStringFactory,
					SqlServerConnectionStringFactory>());

			services.Replace(
				ServiceDescriptor.Scoped<
					IExtendDbContextOptionsBuilder,
					SqlServerDbContextOptionsBuilder>());

			services.AddDbContext<DbContext, ProductCatalogDbContext>((sp, o) =>
			{
				var extendOptionsBuilder = sp.GetRequiredService<IExtendDbContextOptionsBuilder>();
				var connStringFactory = sp.GetRequiredService<IDbConnStringFactory>();
				extendOptionsBuilder.Extend(o, connStringFactory, string.Empty);
			});

			services.Replace(
				ServiceDescriptor.Scoped<
					IRepositoryFactory,
					DefaultRepositoryFactory>());

			services.Replace(
				ServiceDescriptor.Scoped<
					DatabaseMigration,
					SqlServerDatabaseMigration>());

			return services;
		}
	}
}

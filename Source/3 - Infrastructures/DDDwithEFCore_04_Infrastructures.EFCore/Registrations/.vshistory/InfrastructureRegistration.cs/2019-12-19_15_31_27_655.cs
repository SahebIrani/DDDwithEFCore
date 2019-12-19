using DDDwithEFCore_01_Common.Core.Contracts;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.Contracts;
using DDDwithEFCore_02_Common.Infrastructures.EFCore.Migration;
using DDDwithEFCore_02_Common.Infrastructures.EFCore.Repositories;

using DDDwithEFCore_04_Infrastructures.EFCore.Db;
using DDDwithEFCore_04_Infrastructures.EFCore.SqlServer;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Registrations
{
	public static class InfrastructureRegistration
	{
		public static IServiceCollection AddEfCoreSqlServerDb(this IServiceCollection services)
		{
			services.AddDbContextPool<DbContext, AppDbContext>((sp, o) =>
			{
				IExtendDbContextOptionsBuilder extendOptionsBuilder =
					sp.GetRequiredService<IExtendDbContextOptionsBuilder>();

				IDbConnStringFactory connStringFactory =
					sp.GetRequiredService<IDbConnStringFactory>();

				extendOptionsBuilder.Extend(o, connStringFactory, string.Empty);
			});


			services.Replace(
				ServiceDescriptor.Scoped<
					IDbConnStringFactory,
					SqlServerConnectionStringFactory>());

			services.Replace(
				ServiceDescriptor.Scoped<
					IExtendDbContextOptionsBuilder,
					SqlServerDbContextOptionsBuilder>());

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

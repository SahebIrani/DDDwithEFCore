using System.Reflection;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.Contracts;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_04_Infrastructures.EFCore.SqlServer
{
	public sealed class SqlServerDbContextOptionsBuilder : IExtendDbContextOptionsBuilder
	{
		public DbContextOptionsBuilder Extend(DbContextOptionsBuilder optionsBuilder,
										IDbConnStringFactory connectionStringFactory,
										string assemblyName)
		{

			var migrationFromAssemblyName = !string.IsNullOrWhiteSpace(assemblyName)
				? assemblyName
				: Assembly.GetExecutingAssembly().FullName;

			return optionsBuilder.UseSqlServer(
				connectionStringFactory.Create(),
				sqlServerOptionsAction =>
				{
					sqlServerOptionsAction.MigrationsAssembly(migrationFromAssemblyName);
					sqlServerOptionsAction.EnableRetryOnFailure(
						15,
						TimeSpan.FromSeconds(30),
						null);
				}
			);
		}
	}
}

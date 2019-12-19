using System;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.Contracts;

using DDDwithEFCore_04_Infrastructures.EFCore.Db;

using Microsoft.Extensions.Configuration;

namespace DDDwithEFCore_04_Infrastructures.EFCore.SqlServer
{
	public sealed class SqlServerConnectionStringFactory : IDbConnStringFactory
	{
		public SqlServerConnectionStringFactory(IConfiguration configuration) =>
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

		private readonly IConfiguration _configuration;

		public string Create() => _configuration.GetConnectionString(nameof(AppDbContext));
	}
}

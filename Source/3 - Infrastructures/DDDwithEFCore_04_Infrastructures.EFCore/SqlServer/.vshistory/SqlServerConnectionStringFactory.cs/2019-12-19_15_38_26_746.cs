using System;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.Contracts;

using Microsoft.Extensions.Configuration;

namespace DDDwithEFCore_04_Infrastructures.EFCore.SqlServer
{
	public sealed class SqlServerConnectionStringFactory : IDbConnStringFactory
	{
		private readonly IConfiguration _configuration;

		public SqlServerConnectionStringFactory(IConfiguration configuration) =>
			_configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));

		public string Create() => _configuration.GetConnectionString("DefaultDb");
	}
}

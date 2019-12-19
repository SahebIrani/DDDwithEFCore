using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DDDwithEFCore_06_ApplicationServices.Queries.DbConnectionFactories
{
	public sealed class SqlServerDbConnectionFactory : IDisposable
	{
		public SqlServerDbConnectionFactory(string connectionString)
			=> _connectionString = connectionString;

		private readonly string _connectionString;
		private IDbConnection _connection;


		public async Task<IDbConnection> GetConnection(CancellationToken cancellationToken = default)
		{
			if (_connection == null || _connection.State != ConnectionState.Open)
			{
				_connection = new SqlConnection(_connectionString);
				await ((SqlConnection)_connection).OpenAsync(cancellationToken);
			}

			return _connection;
		}

		public void Dispose()
		{
			if (_connection != null && _connection.State == ConnectionState.Open)
				_connection.Dispose();
		}
	}
}

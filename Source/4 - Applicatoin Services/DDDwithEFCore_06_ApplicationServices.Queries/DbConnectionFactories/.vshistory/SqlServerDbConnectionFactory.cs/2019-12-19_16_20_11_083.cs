using System;
using System.Data;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;

namespace DDDwithEFCore_06_ApplicationServices.Queries.DbConnectionFactories.vshistory.SqlServerDbConnectionFactory.cs
{
	public sealed class SqlServerDbConnectionFactory : IDisposable
	{
		private readonly string _connectionString;
		private IDbConnection _connection;

		public SqlServerDbConnectionFactory(string connectionString)
			=> _connectionString = connectionString;

		public async Task<IDbConnection> GetConnection(CancellationToken cancellationToken = default(CancellationToken))
		{
			if (_connection == null || this._connection.State != ConnectionState.Open)
			{
				_connection = new SqlConnection(_connectionString);
				await ((SqlConnection)this._connection).OpenAsync(cancellationToken);
			}

			return this._connection;
		}

		#region Implementation of IDisposable

		public void Dispose()
		{
			if (this._connection != null && this._connection.State == ConnectionState.Open)
			{
				this._connection.Dispose();
			}
		}

		#endregion
	}
}

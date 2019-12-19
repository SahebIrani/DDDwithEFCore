using System;
using DDDEfCore.Infrastructures.EfCore.Common;
using Microsoft.Extensions.Configuration;

namespace DDDwithEFCore_04_Infrastructures.EFCore.SqlServer
{
    public sealed class SqlServerConnectionStringFactory : IDbConnStringFactory
    {
        private readonly IConfiguration _configuration;

        public SqlServerConnectionStringFactory(IConfiguration configuration)
        {
            this._configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        #region Implementation of IDbConnStringFactory

        public string Create()
        {
            return this._configuration.GetConnectionString("DefaultDb");
        }

        #endregion
    }
}

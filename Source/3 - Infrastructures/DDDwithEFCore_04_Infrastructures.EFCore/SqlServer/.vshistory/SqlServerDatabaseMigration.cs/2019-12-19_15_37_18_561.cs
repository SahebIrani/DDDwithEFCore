using System.Linq;
using System.Threading.Tasks;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.Migration;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_04_Infrastructures.EFCore.SqlServer
{
	public sealed class SqlServerDatabaseMigration : DatabaseMigration
	{
		public SqlServerDatabaseMigration(DbContext dbContext) : base(dbContext) { }

		protected override bool HasPendingMigrations()
		{
			string latestAppliedMigrationId =
				DbContext.Database.GetAppliedMigrations().LastOrDefault();

			string latestPendingMigrationId =
				DbContext.Database.GetMigrations().LastOrDefault();

			return string.IsNullOrWhiteSpace(latestAppliedMigrationId) ||
				   (!string.IsNullOrWhiteSpace(latestPendingMigrationId) &&
					latestAppliedMigrationId != latestPendingMigrationId);
		}

		protected override async Task DoMigration() => await DbContext.Database.MigrateAsync();
	}
}

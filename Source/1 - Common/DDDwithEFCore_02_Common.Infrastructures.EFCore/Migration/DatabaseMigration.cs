using System;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Migration
{
	public abstract class DatabaseMigration
	{
		protected DatabaseMigration(DbContext dbContext)
			=> DbContext = dbContext
			?? throw new ArgumentNullException(nameof(dbContext))
			;

		protected DbContext DbContext { get; }

		protected abstract bool HasPendingMigrations();

		protected virtual async Task DoMigration() => await Task.FromResult(true);

		public async Task ApplyMigration()
		{
			if (HasPendingMigrations())
				await DoMigration();
		}
	}
}

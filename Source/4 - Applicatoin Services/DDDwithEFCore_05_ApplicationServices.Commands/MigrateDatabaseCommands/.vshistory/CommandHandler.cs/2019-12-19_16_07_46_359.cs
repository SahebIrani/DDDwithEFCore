using System.Threading;
using System.Threading.Tasks;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.MigrateDatabaseCommands
{
	public class CommandHandler : AsyncRequestHandler<MigrateDatabaseCommand>
	{
		private readonly DatabaseMigration _databaseMigration;

		public CommandHandler(DatabaseMigration databaseMigration)
			=> this._databaseMigration = databaseMigration;

		#region Overrides of AsyncRequestHandler<MigrateDatabaseCommand>

		protected override async Task Handle(MigrateDatabaseCommand request, CancellationToken cancellationToken)
		{
			await this._databaseMigration.ApplyMigration();
		}

		#endregion
	}
}

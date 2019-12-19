using System.Threading;
using System.Threading.Tasks;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.Migration;

using MediatR;

namespace DDDwithEFCore_05_ApplicationServices.Commands.MigrateDatabaseCommands
{
	public class CommandHandler : AsyncRequestHandler<MigrateDatabaseCommand>
	{
		public CommandHandler(DatabaseMigration databaseMigration)
			=> _databaseMigration = databaseMigration;

		private readonly DatabaseMigration _databaseMigration;

		protected override async Task Handle(
			MigrateDatabaseCommand request,
			CancellationToken cancellationToken) =>
			await _databaseMigration.ApplyMigration();
	}
}

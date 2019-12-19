using System;
using System.Threading.Tasks;

using DDDwithEFCore_05_ApplicationServices.Commands.MigrateDatabaseCommands;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DDDEfCore.ProductCatalog.WebApi.Controllers
{
	[Route("[controller]")]
	[ApiExplorerSettings(IgnoreApi = true)]
	public class CommonController : ControllerBase
	{
		public CommonController(IMediator mediator)
			=> Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

		public IMediator Mediator { get; }

		[HttpGet]
		public IActionResult Default() => Ok("Hello SinjulMSBH .. !!!!");

		[HttpGet("migrate")]
		public async Task<IActionResult> Migrate()
		{
			try
			{
				await Mediator.Send(new MigrateDatabaseCommand());
				return Ok("Database has been migrated.");
			}
			catch (Exception e)
			{
				return BadRequest(e);
			}
		}
	}
}
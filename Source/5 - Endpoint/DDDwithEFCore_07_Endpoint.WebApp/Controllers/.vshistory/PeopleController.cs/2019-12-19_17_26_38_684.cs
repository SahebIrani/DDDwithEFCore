using System;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace DDDwithEFCore_07_Endpoint.WebApp.Controllers
{
	[Route("api/[controller]"), ApiController]
	public class PeopleController : ControllerBase
	{
		public PeopleController(IMediator mediator)
			=> Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

		public IMediator Mediator { get; }
	}
}

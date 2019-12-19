using System;
using System.Threading.Tasks;

using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using DDDwithEFCore_05_ApplicationServices.Commands.PersonCommands.CreatePerson;

using MediatR;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DDDwithEFCore_07_Endpoint.WebApp.Controllers
{
	[Route("api/[controller]"), ApiController]
	public class PeopleController : ControllerBase
	{
		public PeopleController(IMediator mediator)
			=> Mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

		public IMediator Mediator { get; }



		/// <summary>
		/// Create Person
		/// </summary>
		/// <param name="command"></param>
		/// <returns></returns>
		[HttpPost]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Create([FromBody] CreatePersonCommand command)
		{
			await Mediator.Send(command);
			return NoContent();
		}

		/// <summary>
		/// Update specific Person
		/// </summary>
		/// <param name="personId"></param>
		/// <param name="personName"></param>
		/// <returns></returns>
		[HttpPut("{Person}")]
		[ProducesResponseType(StatusCodes.Status204NoContent)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> Update(PersonId productId, [FromBody] string personName)
		{
			UpdateProductCommand command = new UpdateProductCommand
			{
				ProductId = productId,
				ProductName = productName
			};
			await this._mediator.Send(command);
			return NoContent();
		}

		/// <summary>
		/// Search Products by Name
		/// </summary>
		/// <param name="searchTerm"></param>
		/// <param name="pageIndex"></param>
		/// <param name="pageSize"></param>
		/// <returns></returns>
		[HttpGet("search")]
		[ProducesResponseType(typeof(GetProductCollectionResult), StatusCodes.Status200OK)]
		[ProducesResponseType(StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> SearchProducts(string searchTerm, int pageIndex = 1, int pageSize = 10)
		{
			var request = new GetProductCollectionRequest
			{
				SearchTerm = searchTerm,
				PageIndex = pageIndex,
				PageSize = pageSize
			};

			var result = await this._mediator.Send(request);
			return Ok(result);
		}

		/// <summary>
		/// Get detail of specific Product
		/// </summary>
		/// <param name="productId"></param>
		/// <returns></returns>
		[HttpGet("{productId}")]
		[ProducesResponseType(typeof(GetProductDetailResult), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(GlobalExceptionHandlerMiddleware.ExceptionResponse), StatusCodes.Status400BadRequest)]
		[ProducesResponseType(typeof(GlobalExceptionHandlerMiddleware.ExceptionResponse), StatusCodes.Status500InternalServerError)]
		public async Task<IActionResult> GetProductDetail(ProductId productId)
		{
			var request = new GetProductDetailRequest { ProductId = productId };
			var result = await this._mediator.Send(request);
			return Ok(result);
		}

	}
}

using MediatR;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class GetPersonDetailRequest : IRequest<GetProductDetailResult>
	{
		public ProductId ProductId { get; set; }
	}
}

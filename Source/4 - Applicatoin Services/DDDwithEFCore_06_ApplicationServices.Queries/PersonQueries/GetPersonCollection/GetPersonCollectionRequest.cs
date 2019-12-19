using MediatR;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonCollection
{
	public class GetPersonCollectionRequest : IRequest<GetPersonCollectionResult>
	{
		public string SearchTerm { get; set; }
		public int PageIndex { get; set; } = 1;
		public int PageSize { get; set; } = 10;
	}
}

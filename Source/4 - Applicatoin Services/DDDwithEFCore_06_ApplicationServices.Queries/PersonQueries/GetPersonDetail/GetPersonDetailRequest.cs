using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using MediatR;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class GetPersonDetailRequest : IRequest<GetPersonDetailResult>
	{
		public PersonId PersonId { get; set; }
	}
}

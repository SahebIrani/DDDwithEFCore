
using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class GetPersonDetailResult
	{
		public PersonDetailResult People { get; set; }

		public class PersonDetailResult
		{
			public PersonId Id { get; set; }
			public string Name { get; set; }
		}
	}
}

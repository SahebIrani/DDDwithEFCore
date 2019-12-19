using System.Collections.Generic;

using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonCollection
{
	public class GetPersonCollectionResult
	{
		public int TotalPerson { get; set; }
		public IEnumerable<PersonCollectionItem> People { get; set; }

		public class PersonCollectionItem
		{
			public PersonId Id { get; set; }
			public string DisplayName { get; set; }
		}
	}
}

using System.Collections.Generic;

using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class GetPersonDetailResult
	{
		public PersonDetailResult Product { get; set; }

		public IEnumerable<CatalogCategoryResult> CatalogCategories { get; set; }


		public class PersonDetailResult
		{
			public PersonId Id { get; set; }
			public string Name { get; set; }
		}

		public class CatalogCategoryResult
		{
			public CatalogCategoryId CatalogCategoryId { get; set; }
			public string CatalogCategoryName { get; set; }
			public CatalogId CatalogId { get; set; }
			public string CatalogName { get; set; }
			public CatalogProductId CatalogProductId { get; set; }
			public string ProductDisplayName { get; set; }
		}
	}
}

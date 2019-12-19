using System.Collections.Generic;

namespace DDDwithEFCore_06_ApplicationServices.Queries.PersonQueries.GetPersonDetail
{
	public class GetPersonDetailResult
	{
		public ProductDetailResult Product { get; set; }

		public IEnumerable<CatalogCategoryResult> CatalogCategories { get; set; }


		public class ProductDetailResult
		{
			public ProductId Id { get; set; }
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

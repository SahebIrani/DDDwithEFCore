using System.Collections.Generic;
using System.Linq;

using Humanizer;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Db
{
	public class ProductCatalogDbContext : ApplicationDbContextBase
	{
		public ProductCatalogDbContext(
			DbContextOptions<ProductCatalogDbContext> dbContextOptions,
			IConfiguration configuration)
			: base(
				  dbContextOptions: dbContextOptions,
				  configuration: configuration)
		{
		}

		#region Overrides of ApplicationDbContextBase

		protected override void RegisterConventions(ModelBuilder builder)
		{
			base.RegisterConventions(builder);

			IEnumerable<IMutableEntityType> types =
				builder.Model.GetEntityTypes()
					.Where(entity => !string.IsNullOrWhiteSpace(entity.ClrType.Namespace))
			;

			foreach (IMutableEntityType entityType in types)
				builder.Entity(entityType.Name).ToTable(entityType.ClrType.Namespace.Pluralize());
		}

		#endregion
	}
}

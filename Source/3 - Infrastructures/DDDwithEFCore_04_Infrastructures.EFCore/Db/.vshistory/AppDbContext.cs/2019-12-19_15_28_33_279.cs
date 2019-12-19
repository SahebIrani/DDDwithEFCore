using System.Collections.Generic;
using System.Linq;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.BaseContext;

using Humanizer;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.Extensions.Configuration;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Db
{
	public class AppDbContext : ApplicationDbContextBase
	{
		public ProductCatalogDbContext(
			DbContextOptions<ProductCatalogDbContext> dbContextOptions,
			IConfiguration configuration)
			: base(
				  dbContextOptions: dbContextOptions,
				  configuration: configuration)
		{
		}

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
	}
}

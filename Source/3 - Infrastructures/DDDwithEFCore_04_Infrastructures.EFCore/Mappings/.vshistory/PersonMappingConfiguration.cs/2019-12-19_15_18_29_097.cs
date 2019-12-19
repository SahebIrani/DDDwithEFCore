using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Mappings
{
	public class ProductMappingConfiguration : IEntityTypeConfiguration<Product>
	{
		#region Implementation of IEntityTypeConfiguration<Product>

		public void Configure(EntityTypeBuilder<Product> builder)
		{
			builder.HasKey(x => x.ProductId);

			builder
				.Property(x => x.ProductId)
				.HasField("Id")
				.HasColumnName("Id")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasConversion(x => x.Id, id => (ProductId)id);
		}

		#endregion
	}
}

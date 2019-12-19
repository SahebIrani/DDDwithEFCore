using DDDwithEFCore_03_ApplicationCore.DomainModels.People;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Mappings
{
	public class ProductMappingConfiguration : IEntityTypeConfiguration<Person>
	{
		public void Configure(EntityTypeBuilder<Person> builder)
		{
			builder.HasKey(x => x.PersonId);

			builder
				.Property(x => x.PersonId)
				.HasField("Id")
				.HasColumnName("Id")
				.UsePropertyAccessMode(PropertyAccessMode.Field)
				.HasConversion(x => x.Id, id => (PersonId)id);
		}
	}
}

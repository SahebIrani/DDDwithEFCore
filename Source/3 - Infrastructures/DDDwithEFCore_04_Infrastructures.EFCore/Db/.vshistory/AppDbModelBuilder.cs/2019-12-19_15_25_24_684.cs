using System.Reflection;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Db
{
	public sealed class ProductCatalogDbModelBuilder : ICustomModelBuilder
	{
		#region Implementation of ICustomModelBuilder

		public void Build(ModelBuilder modelBuilder) =>
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

		#endregion
	}
}

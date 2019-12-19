using System.Reflection;

using DDDwithEFCore_02_Common.Infrastructures.EFCore.Contracts;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_04_Infrastructures.EFCore.Db
{
	public sealed class AppDbModelBuilder : ICustomModelBuilder
	{
		public void Build(ModelBuilder modelBuilder) =>
			modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
	}
}

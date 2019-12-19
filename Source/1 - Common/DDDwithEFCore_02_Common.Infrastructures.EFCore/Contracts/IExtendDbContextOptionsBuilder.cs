using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Contracts
{
	public interface IExtendDbContextOptionsBuilder
	{
		DbContextOptionsBuilder Extend(
			DbContextOptionsBuilder optionsBuilder,
			IDbConnStringFactory connectionStringFactory,
			string assemblyName);
	}
}

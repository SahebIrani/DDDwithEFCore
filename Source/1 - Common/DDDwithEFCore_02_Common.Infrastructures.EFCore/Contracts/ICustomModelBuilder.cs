using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Contracts
{
	public interface ICustomModelBuilder
	{
		void Build(ModelBuilder modelBuilder);
	}
}

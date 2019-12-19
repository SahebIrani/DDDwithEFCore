using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Migration
{
	public interface ISeedData<in TDbContext> where TDbContext : DbContext
	{
		Task SeedAsync(TDbContext context);
	}
}

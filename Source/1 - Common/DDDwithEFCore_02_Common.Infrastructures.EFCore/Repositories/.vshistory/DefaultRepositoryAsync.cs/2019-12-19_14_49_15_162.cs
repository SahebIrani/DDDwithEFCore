using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Repositories
{
	public class DefaultRepositoryAsync<TAggregate>
		: IRepository<TAggregate> where TAggregate : AggregateRoot
	{
		public DefaultRepositoryAsync(DbContext dbContext)
			=> _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

		private readonly DbContext _dbContext;

		#region Implementation of IRepository<TAggregate>

		public IQueryable<TAggregate> AsQueryable() => _dbContext.Set<TAggregate>();

		public async Task<TAggregate> FindOneAsync(Expression<Func<TAggregate, bool>> predicate) =>
			await _dbContext.Set<TAggregate>().FirstOrDefaultAsync(predicate);

		public async Task AddAsync(TAggregate aggregate) => await _dbContext.AddAsync(aggregate);

		public Task UpdateAsync(TAggregate aggregate) => Task.FromResult(_dbContext.Update(aggregate));

		public Task RemoveAsync(TAggregate aggregate) => Task.FromResult(_dbContext.Remove(aggregate));

		#endregion
	}
}

using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_01_Common.Core.Contracts
{
	public interface IRepository<TAggregate> where TAggregate : AggregateRoot
	{
		IQueryable<TAggregate> AsQueryable();
		Task<TAggregate> FindOneAsync(Expression<Func<TAggregate, bool>> predicate);
		Task AddAsync(TAggregate aggregate);
		Task UpdateAsync(TAggregate aggregate);
		Task RemoveAsync(TAggregate aggregate);
	}
}

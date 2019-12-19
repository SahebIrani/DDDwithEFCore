using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

using DDDwithEFCore_01_Common.Core.Contracts;
using DDDwithEFCore_01_Common.Core.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Extensions
{
	public static class RepositoryExtensions
	{
		public static async Task<TEntity> FindOneWithIncludeAsync<TEntity>(
			this IRepository<TEntity> repository,
			Expression<Func<TEntity, bool>> predicate,
			Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null)
			where TEntity : AggregateRoot
		{
			IQueryable<TEntity> queryable = repository.AsQueryable();

			if (include != null)
				queryable = include.Invoke(queryable);

			return await queryable.FirstOrDefaultAsync(predicate);
		}
	}
}

using System;
using System.Collections.Concurrent;

using DDDEfCore.Core.Common;
using DDDEfCore.Core.Common.Models;

using Microsoft.EntityFrameworkCore;

namespace DDDwithEFCore_02_Common.Infrastructures.EFCore.Repositories
{
	public class DefaultRepositoryFactory : IRepositoryFactory
	{
		private readonly DbContext _dbContext;

		private ConcurrentDictionary<Type, object> _repositories;

		public DefaultRepositoryFactory(DbContext dbContext)
			=> _dbContext = dbContext ??
				throw new ArgumentNullException(nameof(dbContext))
			;

		#region Implementation of IRepositoryFactory

		public IRepository<TAggregate> CreateRepository<TAggregate>() where TAggregate : AggregateRoot
		{
			if (_repositories == null)
				_repositories = new ConcurrentDictionary<Type, object>();

			if (!_repositories.ContainsKey(typeof(TAggregate)))
				_repositories[typeof(TAggregate)] =
					new DefaultRepositoryAsync<TAggregate>(_dbContext);

			return (IRepository<TAggregate>)_repositories[typeof(TAggregate)];
		}

		#endregion

		#region Implementation of IDisposable

		public void Dispose() => _dbContext?.SaveChanges();

		#endregion
	}
}

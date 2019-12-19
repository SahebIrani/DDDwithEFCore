using System;
using System.Collections.Concurrent;

using DDDwithEFCore_01_Common.Core.Contracts;
using DDDwithEFCore_01_Common.Core.Models;

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


		public IRepository<TAggregate> CreateRepository<TAggregate>() where TAggregate : AggregateRoot
		{
			if (_repositories == null)
				_repositories = new ConcurrentDictionary<Type, object>();

			if (!_repositories.ContainsKey(typeof(TAggregate)))
				_repositories[typeof(TAggregate)] =
					new DefaultRepositoryAsync<TAggregate>(_dbContext);

			return (IRepository<TAggregate>)_repositories[typeof(TAggregate)];
		}


		private bool disposedValue = false;

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
					_dbContext?.SaveChanges();

				disposedValue = true;
			}
		}

		public void Dispose()
		{
			Dispose(true);
		}
	}
}

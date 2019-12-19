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

		#region IDisposable Support
		private bool disposedValue = false; // To detect redundant calls

		protected virtual void Dispose(bool disposing)
		{
			if (!disposedValue)
			{
				if (disposing)
				{
					// TODO: dispose managed state (managed objects).
				}

				// TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
				// TODO: set large fields to null.

				disposedValue = true;
			}
		}

		// TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
		// ~DefaultRepositoryFactory()
		// {
		//   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
		//   Dispose(false);
		// }

		// This code added to correctly implement the disposable pattern.
		public void Dispose()
		{
			// Do not change this code. Put cleanup code in Dispose(bool disposing) above.
			Dispose(true);
			// TODO: uncomment the following line if the finalizer is overridden above.
			// GC.SuppressFinalize(this);
		}
		#endregion


	}
}

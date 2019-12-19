using System;

using DDDwithEFCore_01_Common.Core.Models;

namespace DDDwithEFCore_01_Common.Core.Contracts
{
	public interface IRepositoryFactory : IDisposable
	{
		IRepository<TAggregate> CreateRepository<TAggregate>()
			where TAggregate : AggregateRoot;
	}
}

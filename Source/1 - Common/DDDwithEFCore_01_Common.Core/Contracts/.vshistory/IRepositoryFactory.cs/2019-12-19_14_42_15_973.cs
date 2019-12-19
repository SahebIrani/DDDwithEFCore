using System;

using DDDwithEFCore_00_CrossCutting.Models;

namespace DDDwithEFCore_01_Common.Core.Contracts
{
	public interface IRepositoryFactory : IDisposable
	{
		IRepository<TAggregate> CreateRepository<TAggregate>()
			where TAggregate : AggregateRoot;
	}
}

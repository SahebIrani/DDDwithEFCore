using System;
using DDDEfCore.Core.Common.Models;

namespace DDDwithEFCore_01_Common.Core.Contracts.vshistory.IRepositoryFactory.cs
{
    public interface IRepositoryFactory: IDisposable
    {
        IRepository<TAggregate> CreateRepository<TAggregate>() where TAggregate : AggregateRoot;
    }
}

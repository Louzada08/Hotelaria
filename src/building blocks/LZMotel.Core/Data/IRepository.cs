using System;
using LZMotel.Core.DomainObjects;

namespace LZMotel.Core.Data
{
    public interface IRepository<T> : IDisposable where T : IAggregateRoot
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
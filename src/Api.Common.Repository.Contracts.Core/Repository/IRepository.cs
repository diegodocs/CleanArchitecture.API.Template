using System;
using Api.Common.Repository.Contracts.Core.Entities;

namespace Api.Common.Repository.Contracts.Core.Repository
{
    public interface IRepository<TEntity> :
        IPersistenceService<TEntity>,
        IQueryService<TEntity>, IDisposable where TEntity : IDomainEntity
    {
    }
}
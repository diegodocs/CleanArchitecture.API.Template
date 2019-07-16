using Api.Common.Repository.Contracts.Core.Entities;
using System;

namespace Api.Common.Repository.Contracts.Core.Repository
{
    public interface IRepository<TEntity> :
        IPersistenceService<TEntity>,
        IQueryService<TEntity>, IDisposable where TEntity : IDomainEntity
    {
    }
}
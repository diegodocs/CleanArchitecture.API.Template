using Api.Common.Repository.Contracts.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Api.Common.Repository.Contracts.Core.Repository
{
    public interface IPersistenceService<TEntity> where TEntity : IDomainEntity
    {
        void Insert(TEntity instance);

        void Insert(IEnumerable<TEntity> instances);

        void Delete(IEnumerable<Guid> ids);

        void Delete(Expression<Func<TEntity, bool>> expression);

        void Delete(Guid id);

        void Update(TEntity instance);

        void Update(IEnumerable<TEntity> instances);
    }
}
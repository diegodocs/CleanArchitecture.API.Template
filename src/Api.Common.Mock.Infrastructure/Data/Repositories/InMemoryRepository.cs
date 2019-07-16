using Api.Common.Repository.Contracts.Core.Entities;
using Api.Common.Repository.Contracts.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Api.Common.Mock.Infrastructure.Data.Repositories
{
    public class InMemoryRepository<TEntity> : IRepository<TEntity> where TEntity : IDomainEntity
    {
        public InMemoryRepository()
        {
            Repository = new List<TEntity>();
        }

        public void Dispose()
        {
            // Cleanup
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            Repository = null;
        }

        public List<TEntity> Repository { get; protected set; }

        public void Update(TEntity instance)
        {
            instance.ModifiedDate = DateTime.UtcNow;

            Delete(instance.Id);
            Repository.Add(instance);
        }

        public void Delete(Guid id)
        {
            Repository.RemoveAll(x => x.Id == id);
        }

        public void Insert(TEntity instance)
        {
            if (instance.Id == Guid.Empty)
            {
                instance.Id = Guid.NewGuid();
            }

            instance.CreateDate = DateTime.UtcNow;

            Repository.Add(instance);
        }

        public void Insert(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances)
            {
                Insert(instance);
            }
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                Delete(id);
            }
        }

        public void Update(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances)
            {
                Update(instance);
            }
        }

        public TEntity Find(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.Any() ? Repository.AsQueryable().First(expression) : default(TEntity);
        }

        public IEnumerable<TEntity> All()
        {
            return Repository;
        }

        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.AsQueryable().Where(expression);
        }

        public IEnumerable<TEntity> FindListWithNoTracking(Expression<Func<TEntity, bool>> expression)
        {
            return Repository.AsQueryable().Where(expression);
        }

        public TEntity FindById(Guid id)
        {
            return Repository.AsQueryable().FirstOrDefault(x => x.Id == id);
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            Repository.RemoveAll(expression.Compile().Invoke);
        }

        public void BulkInsert(IList<TEntity> instances)
        {
            Repository.AddRange(instances);
        }
    }
}
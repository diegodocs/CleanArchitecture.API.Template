using Api.Common.Repository.Contracts.Core.Entities;
using Api.Common.Repository.Contracts.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Api.Common.Mock.Infrastructure.Data.Repositories
{
    public class InMemoryRepositorySoftDelete<TEntity> : IRepository<TEntity> where TEntity : IDomainEntity
    {
        public InMemoryRepositorySoftDelete()
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

            Repository.RemoveAll(x => x.Id == instance.Id);
            Repository.Add(instance);
        }

        public void Delete(Guid id)
        {
            var instance = FindById(id);

            if (instance == null)
                return;

            instance.IsActive = false;

            Update(instance);
        }

        public void Insert(TEntity instance)
        {
            if (instance.Id == Guid.Empty) instance.Id = Guid.NewGuid();

            instance.CreateDate = DateTime.UtcNow;
            instance.IsActive = true;

            Repository.Add(instance);
        }

        public void Insert(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances)
                Insert(instance);
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
                Delete(id);
        }

        public void Update(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances)
                Update(instance);
        }

        public TEntity Find(Expression<Func<TEntity, bool>> expression)
        {
            var query = Repository.AsQueryable().Where(expression);
            return query.FirstOrDefault(x => x.IsActive);
        }

        public IEnumerable<TEntity> All()
        {
            return Repository;
        }

        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> expression)
        {
            var query = Repository.AsQueryable().Where(expression);
            return query.Where(x => x.IsActive);
        }

        public IEnumerable<TEntity> FindListWithNoTracking(Expression<Func<TEntity, bool>> expression)
        {
            var query = Repository.AsQueryable().Where(expression);
            return query.Where(x => x.IsActive);
        }

        public TEntity FindById(Guid id)
        {
            return Repository.AsQueryable().FirstOrDefault(x => x.Id == id && x.IsActive);
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
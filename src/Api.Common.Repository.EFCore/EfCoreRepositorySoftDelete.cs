using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using EFCore.BulkExtensions;
using Api.Common.Repository.Contracts.Core.Entities;
using Api.Common.Repository.Contracts.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace Api.Common.Repository.EFCore
{
    public class EfCoreRepositorySoftDelete<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public EfCoreRepositorySoftDelete(DbContext context)
        {
            this.context = context;
            dbSet = this.context.Set<TEntity>();
        }

        public IEnumerable<TEntity> All()
        {
            return dbSet.ToArray();
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
                DeleteInstance(id);

            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            DeleteInstance(id);
            context.SaveChanges();
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {            
            var query = dbSet.Where(expression);            
            var instances = query.Where(x => x.IsActive).ToArray();

            foreach (var instance in instances)
                DeleteInstance(instance.Id);

            if (instances.Any())
                context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> expression)
        {            
            var query = dbSet.Where(expression);
            return query.FirstOrDefault(x => x.IsActive);
        }

        public TEntity FindById(Guid id)
        {
            return Find(x => x.Id == id);
        }

        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> expression)
        {
            var query = dbSet.Where(expression);
            return query.Where(x => x.IsActive).ToArray();
        }

        public void Insert(TEntity instance)
        {
            instance.Id = Guid.NewGuid();
            instance.CreateDate = DateTime.UtcNow;
            instance.IsActive = true;

            dbSet.Add(instance);
            context.SaveChanges();
        }

        public void Insert(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances)
                dbSet.Add(instance);

            context.SaveChanges();
        }

        public void BulkInsert(IList<TEntity> instances)
        {
            context.BulkInsert(instances);
        }

        public void Update(TEntity instance)
        {
            instance.ModifiedDate = DateTime.UtcNow;

            UpdateInstance(instance);
            context.SaveChanges();
        }

        public void Update(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances)
                UpdateInstance(instance);

            context.SaveChanges();
        }

        private void DeleteInstance(Guid id)
        {
            var instance = dbSet.Find(id);
            instance.IsActive = false;

            UpdateInstance(instance);
        }

        private void UpdateInstance(TEntity instance)
        {
            instance.ModifiedDate = DateTime.UtcNow;            
            dbSet.Update(instance);
        }
    }
}
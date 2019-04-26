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
    public class EfCoreRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public EfCoreRepository(DbContext context)
        {
            this.context = context;           
            this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.context.ChangeTracker.LazyLoadingEnabled = false;
            dbSet = this.context.Set<TEntity>();
        }

        public IEnumerable<TEntity> All()
        {
            return dbSet.AsEnumerable();
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids) DeleteInstance(id);
            context.SaveChanges();
        }

        public void Delete(Guid id)
        {
            DeleteInstance(id);
            context.SaveChanges();
        }

        public void Delete(Expression<Func<TEntity, bool>> expression)
        {
            var instances = dbSet.Where(expression).ToArray();
            foreach (var instance in instances) DeleteInstance(instance.Id);

            if(instances.Count() > 0)
                context.SaveChanges();
        }

        public void Dispose()
        {
            context.Dispose();
        }

        public TEntity Find(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.FirstOrDefault(expression);
        }

        public TEntity FindById(Guid id)
        {
            return dbSet.Find(id);
        }

        public IEnumerable<TEntity> FindList(Expression<Func<TEntity, bool>> expression)
        {
            return dbSet.Where(expression).ToArray();
        }

        public void Insert(TEntity instance)
        {
            instance.Id = Guid.NewGuid();
            instance.CreateDate = DateTime.UtcNow;

            dbSet.Add(instance);
            context.SaveChanges();
        }

        public void Insert(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances) dbSet.Add(instance);

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
            foreach (var instance in instances) UpdateInstance(instance);

            context.SaveChanges();
        }

        private void DeleteInstance(Guid id)
        {
            //var instance = (TEntity)Activator.CreateInstance(typeof(TEntity), null);
            //instance.Id = id;

            //context.Entry(instance).State = EntityState.Deleted;
            dbSet.Remove(dbSet.Find(id));
        }

        private void UpdateInstance(TEntity instance)
        {
            instance.ModifiedDate = DateTime.UtcNow;
            //context.Entry(instance).State = EntityState.Modified;
            dbSet.Update(instance);
        }
    }
}
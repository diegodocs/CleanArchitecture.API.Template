using Api.Common.Repository.Contracts.Core.Entities;
using Api.Common.Repository.Contracts.Core.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Api.Common.Repository.EFCore
{
    public class EfCoreRepository<TEntity> : IRepository<TEntity> where TEntity : DomainEntity
    {
        protected readonly DbContext context;
        protected readonly DbSet<TEntity> dbSet;

        public void Dispose()
        {
            // Cleanup
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            //Commit
            context.SaveChanges();

            // Cleanup
            context.Dispose();
        }

        public EfCoreRepository(DbContext context)
        {
            this.context = context;
            this.context.ChangeTracker.AutoDetectChangesEnabled = false;
            this.context.ChangeTracker.LazyLoadingEnabled = false;
            this.context.ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
            dbSet = this.context.Set<TEntity>();
        }

        public IEnumerable<TEntity> All()
        {
            return dbSet.ToArray();
        }

        public void Delete(IEnumerable<Guid> ids)
        {
            foreach (var id in ids)
            {
                DeleteInstance(id);
            }

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
            foreach (var instance in instances)
            {
                DeleteInstance(instance);
            }

            if (instances.Any())
            {
                context.SaveChanges();
            }
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
            foreach (var instance in instances)
            {
                instance.Id = Guid.NewGuid();
                instance.CreateDate = DateTime.UtcNow;

                dbSet.Add(instance);
            }

            context.SaveChanges();
        }

        public void Update(TEntity instance)
        {
            UpdateInstance(instance);
            context.SaveChanges();
        }

        public void Update(IEnumerable<TEntity> instances)
        {
            foreach (var instance in instances)
            {
                UpdateInstance(instance);
            }

            context.SaveChanges();
        }

        private void DeleteInstance(Guid id)
        {
            var instance = FindById(id);

            if (instance != null)
                DeleteInstance(instance);
        }

        private void DeleteInstance(TEntity instance)
        {
            dbSet.Remove(instance);
        }

        private void UpdateInstance(TEntity instance)
        {
            instance.ModifiedDate = DateTime.UtcNow;
            dbSet.Update(instance);
        }
    }
}
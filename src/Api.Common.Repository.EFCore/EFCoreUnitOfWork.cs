using Api.Common.Repository.Contracts.Core.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Data;
using System.Linq;

namespace Api.Common.Repository.EFCore
{
    public class EFCoreUnitOfWork : IUnitOfWork
    {
        private readonly DbContext context;

        public IDbConnection Connection { get; }

        private IDbContextTransaction transaction;

        public EFCoreUnitOfWork(DbContext context)
        {
            this.context = context;
        }

        public void BeginTran()
        {
            transaction = context.Database.BeginTransaction();
        }

        public void Commit()
        {
            context.SaveChanges();
            transaction?.Commit();
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
            transaction?.Dispose();
            context?.Dispose();
        }

        public void Rollback()
        {
            transaction?.Rollback();
            DiscardChanges();
        }

        public void DiscardChanges()
        {
            var arr = context.ChangeTracker.Entries().ToArray();

            for (int i = 0; i < arr.Count(); i++)
            {
                arr[i].State = EntityState.Detached;
            }
        }
    }
}
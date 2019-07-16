using Api.Common.Repository.Contracts.Core.Repository;
using System;
using System.Data;

namespace Api.Common.Mock.Infrastructure.Data.Repositories
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection => throw new NotImplementedException();

        public void Dispose()
        {
            // Cleanup
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            // Cleanup
            Console.WriteLine("Dispose");
        }

        public void BeginTran()
        {
            Console.WriteLine("BeginTran");
        }

        public void Commit()
        {
            Console.WriteLine("Commit");
        }

        public void Rollback()
        {
            Console.WriteLine("Rollback");
        }
    }
}
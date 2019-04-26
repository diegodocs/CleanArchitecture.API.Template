using System;
using System.Data;
using Api.Common.Repository.Contracts.Core.Repository;

namespace Api.Common.Mock.Infrastructure.Data.Repositories
{
    public class InMemoryUnitOfWork : IUnitOfWork
    {
        public IDbConnection Connection => throw new NotImplementedException();

        public void BeginTran()
        {
            Console.WriteLine("BeginTran");
        }

        public void Commit()
        {
            Console.WriteLine("Commit");
        }

        public void Dispose()
        {
            Console.WriteLine("Dispose");
        }

        public void Rollback()
        {
            Console.WriteLine("Rollback");
        }
    }
}
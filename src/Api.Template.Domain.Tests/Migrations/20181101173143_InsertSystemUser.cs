using System;
using System.Collections.Generic;
using Api.Template.Domain.Models;
using Api.Common.Repository.Contracts.Core.Repository;

namespace Api.Template.Domain.Tests.Migrations
{
    public class InsertSystemUser : IDatabaseMigration
    {
        private readonly IRepository<User> repository;

        public InsertSystemUser(IRepository<User> repository)
        {
            this.repository = repository;
        }

        public void Up()
        {

            var listUser = new List<User>
            {
                new UserDefinition().SystemAppUser
            };

            repository.Insert(listUser);
        }
    }
}
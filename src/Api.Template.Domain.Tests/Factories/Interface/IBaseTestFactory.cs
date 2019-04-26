using System;
using Api.Common.Repository.Contracts.Core.Entities;

namespace Api.Template.Domain.Tests.Factories.Interface
{
    public interface IBaseTestFactory<out TEntity> where TEntity : IApplicationViewModel
    {
        TEntity Create();
        void Delete(Guid id);
    }
}
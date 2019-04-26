using System;

namespace Api.Common.Repository.Contracts.Core.Entities
{
    public interface IDomainEntity :
        IEntityWithPrimaryKey<Guid>,
        IEntityWithAudit
    {
    }
}
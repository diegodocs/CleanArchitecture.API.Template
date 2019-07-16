using System;

namespace Api.Common.Repository.Contracts.Core.Entities
{
    public interface IEntityWithAudit
    {
        DateTime CreateDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        Guid AuditUserId { get; set; }
        bool IsActive { get; set; }
    }
}
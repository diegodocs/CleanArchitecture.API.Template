using System;
using System.ComponentModel.DataAnnotations;

namespace Api.Common.Repository.Contracts.Core.Entities
{
    public abstract class DomainEntity : IDomainEntity
    {
        [Key] public Guid Id { get; set; }

        [Required] public DateTime CreateDate { get; set; }

        public DateTime? ModifiedDate { get; set; }

        [Required] public Guid AuditUserId { get; set; }

        [Required] public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"Type:{GetType().Name} - Id:{Id}";
        }
    }
}
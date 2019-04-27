using System;
using Api.Common.Cqrs.Core.Events;

namespace Api.Template.Domain.Events
{
    public class FundGroupCreatedEvent : Event
    {
        public FundGroupCreatedEvent(Guid id, string name, string description, bool isActive, Guid userId, DateTime createDate, DateTime modifiedDate)
        {
            Id = id;
            Name = name;
            Description = description;
            IsActive = isActive;
            UserId = userId;
            CreateDate = createDate;
            ModifiedDate = modifiedDate;
        }

        public Guid Id { get; protected set; }
        public DateTime CreateDate { get; protected set; }
        public DateTime ModifiedDate { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public bool IsActive { get; protected set; }
        public Guid UserId { get; protected set; }
    }
}
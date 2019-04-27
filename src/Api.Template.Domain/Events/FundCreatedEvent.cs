using System;
using Api.Common.Cqrs.Core.Events;

namespace Api.Template.Domain.Events
{
    public class FundCreatedEvent : Event
    {
        public FundCreatedEvent(Guid id, string name, string description, DateTime createDate, DateTime modifiedDate)
        {
            Id = id;
            Name = name;
            Description = description;
            CreateDate = createDate;
            ModifiedDate = modifiedDate;
        }

        public Guid Id { get; protected set; }
        public DateTime CreateDate { get; protected set; }
        public DateTime ModifiedDate { get; protected set; }
        public string Name { get; protected set; }
        public string Description { get; protected set; }
    }
}
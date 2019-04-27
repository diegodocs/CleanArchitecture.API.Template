using System;

namespace Api.Common.Cqrs.Core.Bus
{
    public interface IMessage
    {
        Guid MessageId { get; }
        string MessageType { get; }
        DateTime MessageCreatedDate { get; }
    }
}
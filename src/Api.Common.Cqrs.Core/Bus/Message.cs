using Newtonsoft.Json;
using System;

namespace Api.Common.Cqrs.Core.Bus
{
    public abstract class Message : IMessage
    {
        protected Message()
        {
            MessageId = Guid.NewGuid();
            MessageType = GetType().Name;
            MessageCreatedDate = DateTime.UtcNow;
        }

        [JsonIgnore]
        public Guid MessageId { get; protected set; }

        [JsonIgnore]
        public Guid AuditUserId { get; set; }

        [JsonIgnore]
        public string MessageType { get; protected set; }

        [JsonIgnore]
        public DateTime MessageCreatedDate { get; protected set; }

        public override string ToString()
        {
            return $"MessageId:{MessageId} - MessageType:{MessageType} - TimeStamp:{MessageCreatedDate}";
        }
    }
}
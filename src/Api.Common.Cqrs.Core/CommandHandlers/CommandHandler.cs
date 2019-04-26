using System.Collections.Generic;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.Cqrs.Core.Events;

namespace Api.Common.Cqrs.Core.CommandHandlers
{
    public abstract class CommandHandler
    {
        private readonly IMessageBus bus;

        protected CommandHandler(IMessageBus bus)
        {
            this.bus = bus;
        }

        public void PublishEvents(IEnumerable<IEvent> eventsToPublish)
        {
            foreach (var evt in eventsToPublish) bus.PublishEvent(evt);
        }
    }
}
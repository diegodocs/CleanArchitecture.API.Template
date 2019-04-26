using System.Threading.Tasks;
using Api.Template.Domain.Events;
using Api.Common.Cqrs.Core.Events;

namespace Api.Template.Domain.EventHandlers
{
    public class FundCreatedEventHandler : IEventHandler<FundCreatedEvent>
    {
        public Task Handle(FundCreatedEvent @event)
        {
            // TODO: Implement real event

            return Task.CompletedTask;
        }
    }
}
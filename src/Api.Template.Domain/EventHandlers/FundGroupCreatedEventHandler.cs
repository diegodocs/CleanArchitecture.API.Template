using System.Threading.Tasks;
using Api.Template.Domain.Events;
using Api.Common.Cqrs.Core.Events;

namespace Api.Template.Domain.EventHandlers
{
    public class FundGroupCreatedEventHandler : IEventHandler<FundGroupCreatedEvent>
    {
        public Task Handle(FundGroupCreatedEvent @event)
        {
            // TODO: Implement real event

            return Task.CompletedTask;
        }
    }
}
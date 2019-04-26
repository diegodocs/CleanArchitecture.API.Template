using Api.Common.Cqrs.Core.Commands;
using Api.Common.Cqrs.Core.Events;

namespace Api.Common.Cqrs.Core.Bus
{
    public interface IMessageBus : ICommandDispatcher, IEventPublisher
    {
    }
}
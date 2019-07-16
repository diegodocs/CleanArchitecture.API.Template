using Api.Common.Cqrs.Core.Bus;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Cqrs.Core.Commands;
using Api.Common.Cqrs.Core.Events;
using Api.Common.Repository.Contracts.Core.Entities;
using Api.Common.Repository.Contracts.Core.Validations;
using Autofac;
using System.Threading.Tasks;

namespace Api.Common.Mock.Infrastructure.Bus
{
    public class InMemoryMessageBus : IMessageBus
    {
        private readonly IComponentContext context;

        public InMemoryMessageBus(IComponentContext context)
        {
            this.context = context;
        }

        public Task<TEntity> DispatchCommandTwoWay<TCommand, TEntity>(TCommand command)
            where TCommand : ICommand where TEntity : IDomainEntity
        {
            command.RaiseExceptionIfModelIsNotValid();
            return context.Resolve<ICommandHandlerTwoWay<TCommand, TEntity>>().Handle(command);
        }

        public Task DispatchCommand<TCommand>(TCommand command) where TCommand : ICommand
        {
            command.RaiseExceptionIfModelIsNotValid();
            return context.Resolve<ICommandHandler<TCommand>>().Handle(command);
        }

        public Task PublishEvent<TEvent>(TEvent @event) where TEvent : IEvent
        {
            @event.RaiseExceptionIfModelIsNotValid();
            return context.Resolve<IEventHandler<TEvent>>().Handle(@event);
        }
    }
}
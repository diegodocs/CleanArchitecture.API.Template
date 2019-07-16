using Api.Common.Repository.Contracts.Core.Entities;
using System.Threading.Tasks;

namespace Api.Common.Cqrs.Core.Commands
{
    public interface ICommandDispatcher
    {
        Task<TEntity> DispatchCommandTwoWay<TCommand, TEntity>(TCommand command)
            where TCommand : ICommand
            where TEntity : IDomainEntity;

        Task DispatchCommand<TCommand>(TCommand command)
            where TCommand : ICommand;
    }
}
using System.Threading.Tasks;
using Api.Common.Cqrs.Core.Commands;
using Api.Common.Repository.Contracts.Core.Entities;

namespace Api.Common.Cqrs.Core.CommandHandlers
{
    public interface ICommandHandlerTwoWay<in TCommand, TDomainEntity>
        where TCommand : ICommand
        where TDomainEntity : IDomainEntity
    {
        Task<TDomainEntity> Handle(TCommand command);
    }
}
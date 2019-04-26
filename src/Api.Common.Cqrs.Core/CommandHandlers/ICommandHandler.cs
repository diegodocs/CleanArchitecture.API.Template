using System.Threading.Tasks;
using Api.Common.Cqrs.Core.Commands;

namespace Api.Common.Cqrs.Core.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
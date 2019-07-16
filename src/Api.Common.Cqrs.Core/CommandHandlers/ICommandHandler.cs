using Api.Common.Cqrs.Core.Commands;
using System.Threading.Tasks;

namespace Api.Common.Cqrs.Core.CommandHandlers
{
    public interface ICommandHandler<in TCommand> where TCommand : ICommand
    {
        Task Handle(TCommand command);
    }
}
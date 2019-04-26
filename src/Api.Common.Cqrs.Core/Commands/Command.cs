using Api.Common.Cqrs.Core.Bus;

namespace Api.Common.Cqrs.Core.Commands
{
    public abstract class Command : Message, ICommand
    {
    }
}
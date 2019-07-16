using Api.Template.Domain.Commands.ReleaseCallsStatus;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;
using System.Threading.Tasks;

namespace Api.Template.Domain.CommandHandlers.ReleaseCallsStatus
{
    public class DeleteReleaseCallStatusCommandHandler :
        ICommandHandler<DeleteReleaseCallStatusCommand>
    {
        private readonly IRepository<ReleaseCallStatus> repository;

        public DeleteReleaseCallStatusCommandHandler(IRepository<ReleaseCallStatus> repository)
        {
            this.repository = repository;
        }

        public Task Handle(DeleteReleaseCallStatusCommand command)
        {
            //Persistence
            repository.Delete(command.Id);

            return Task.CompletedTask;
        }
    }
}
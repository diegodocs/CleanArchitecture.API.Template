using Api.Template.Domain.Commands.ReleaseCallsStatus;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;
using System.Threading.Tasks;

namespace Api.Template.Domain.CommandHandlers.ReleaseCallsStatus
{
    public class UpdateReleaseCallStatusCommandHandler :
        ICommandHandlerTwoWay<UpdateReleaseCallStatusCommand, ReleaseCallStatus>
    {
        private readonly IRepository<ReleaseCallStatus> repository;

        public UpdateReleaseCallStatusCommandHandler(IRepository<ReleaseCallStatus> repository)
        {
            this.repository = repository;
        }

        public Task<ReleaseCallStatus> Handle(UpdateReleaseCallStatusCommand command)
        {
            return Task.Run(() =>
            {
                //Domain Changes
                var instance = repository.FindById(command.Id);
                instance.Name = command.Name;

                //Persistence
                repository.Update(instance);

                return instance;
            });
        }
    }
}
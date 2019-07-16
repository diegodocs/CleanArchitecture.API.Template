using Api.Template.Domain.Commands.ReleaseCallsStatus;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;
using System.Threading.Tasks;

namespace Api.Template.Domain.CommandHandlers.ReleaseCallsStatus
{
    public class CreateReleaseCallStatusCommandHandler :
        ICommandHandlerTwoWay<CreateReleaseCallStatusCommand, ReleaseCallStatus>
    {
        private readonly IRepository<ReleaseCallStatus> repository;

        public CreateReleaseCallStatusCommandHandler(IRepository<ReleaseCallStatus> repository)
        {
            this.repository = repository;
        }

        public Task<ReleaseCallStatus> Handle(CreateReleaseCallStatusCommand command)
        {
            return Task.Run(() =>
            {
                //Domain Changes
                var instance = new ReleaseCallStatus
                {
                    Name = command.Name
                };

                //Persistence
                repository.Insert(instance);

                return instance;
            });
        }
    }
}
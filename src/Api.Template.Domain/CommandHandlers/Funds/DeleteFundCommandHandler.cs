using System.Threading.Tasks;
using Api.Template.Domain.Commands.Funds;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;

namespace Api.Template.Domain.CommandHandlers.Funds
{
    public class DeleteFundCommandHandler :
        CommandHandler,
        ICommandHandler<DeleteFundCommand>
    {
        private readonly IRepository<Fund> fundRepository;

        public DeleteFundCommandHandler(IRepository<Fund> fundRepository, IMessageBus bus) : base(bus)
        {
            this.fundRepository = fundRepository;
        }

        public Task Handle(DeleteFundCommand command)
        {
            //Persistence
            fundRepository.Delete(command.Id);

            return Task.CompletedTask;
        }
    }
}
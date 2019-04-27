using System.Threading.Tasks;
using Api.Template.Domain.Commands.Funds;
using Api.Template.Domain.Models;
using Api.Common.Cqrs.Core.Bus;
using Api.Common.Cqrs.Core.CommandHandlers;
using Api.Common.Repository.Contracts.Core.Repository;

namespace Api.Template.Domain.CommandHandlers.Funds
{
    public class CreateFundCommandHandler :
        CommandHandler,
        ICommandHandlerTwoWay<CreateFundCommand, Fund>
    {
        private readonly IRepository<Fund> fundRepository;

        public CreateFundCommandHandler(IRepository<Fund> fundRepository, IMessageBus bus) : base(bus)
        {
            this.fundRepository = fundRepository;
        }

        public Task<Fund> Handle(CreateFundCommand command)
        {
            return Task.Run(() =>
            {
                //Domain Changes
                var fund = new Fund
                {
                    Name = command.Name,
                    Description = command.Description               
                };

                //Persistence
                fundRepository.Insert(fund);

                return fund;
            });
        }
    }
}